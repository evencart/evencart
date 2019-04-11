using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Products;
using EvenCart.Services.Promotions;
using EvenCart.Services.Purchases;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects;
using EvenCart.Models.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    [Route("Cart")]
    public class CartController : FoundationController
    {
        private readonly ICartService _cartService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IProductService _productService;
        private readonly IProductVariantService _productVariantService;
        private readonly ICartItemService _cartItemService;
        private readonly OrderSettings _orderSettings;

        public CartController(ICartService cartService, IDataSerializer dataSerializer, IProductService productService, IProductVariantService productVariantService, ICartItemService cartItemService, OrderSettings orderSettings)
        {
            _cartService = cartService;
            _dataSerializer = dataSerializer;
            _productService = productService;
            _productVariantService = productVariantService;
            _cartItemService = cartItemService;
            _orderSettings = orderSettings;
        }

        [HttpGet("", Name = RouteNames.Cart)]
        public IActionResult Index()
        {
            return R.Success.Result;
        }

        [DualPost("add", Name = RouteNames.AddToCart, OnlyApi = true)]
        public IActionResult AddToCart(CartItemModel cartItemModel)
        {
            //check if we need to redirect user to login page. This will happen in the following cases.
            //1. Either user is trying to add to his wishlist
            //2. Order settings don't allow guest checkout
            var currentUser = ApplicationEngine.CurrentUser;
            if (currentUser == null || currentUser.IsVisitor())
            {
                if (cartItemModel.IsWishlist || !_orderSettings.AllowGuestCheckout)
                {
                    var loginUrl = ApplicationEngine.RouteUrl(RouteNames.Login);
                    return R.Redirect(loginUrl).Result;
                }
            }
            
            //first get the product
            var product = _productService.Get(cartItemModel.ProductId);
            if (product == null || !product.IsPublic())
                return R.Fail.Result;
            //check appropriate attributes if not wishlist
            ProductVariant variant = null;
            IActionResult validationResult = null;
            string attributeJson = string.Empty;
            if (!cartItemModel.IsWishlist)
            {
                //use attributes which are valid
                var allowedAttributes = product.ProductAttributes.Select(x => x.Label).ToList();
                cartItemModel.Attributes = cartItemModel.Attributes.Where(x => allowedAttributes.Contains(x.Name)).ToList();

                //exclude attributes without any values
                cartItemModel.Attributes = cartItemModel.Attributes
                    .Where(x => x.SelectedValues.Count > 0 && x.SelectedValues.Count(y => y.Name == "-1") == 0)
                    .ToList();
                //check if required attributes have been passed
                var requiredAttributes = product.ProductAttributes.Where(x => x.IsRequired);
                foreach (var ra in requiredAttributes)
                {
                    if (cartItemModel.Attributes.All(x => x.Name != ra.Label))
                        return R.Fail.With("error", T("{0} is required", arguments: ra.AvailableAttribute.Name)).Result;
                }

                //check if valid values for attributes has been passed
                foreach (var ca in cartItemModel.Attributes)
                {
                    var allowedAttributeValues = product.ProductAttributes.First(x => x.Label == ca.Name).AvailableAttribute
                        .AvailableAttributeValues.Select(x => x.Value).ToList();
                    var invalidValues = ca.SelectedValues.Where(x => !allowedAttributeValues.Contains(x.Name)).Select(x => x.Name).ToList();
                    if (invalidValues.Any())
                    {
                        return R.Fail.With("error",
                            T("'{0}' is not valid value for '{1}'",
                                arguments: new object[] { string.Join(T(" or "), invalidValues), ca.Name })).Result;
                    }
                }

                //validate the quantity
                ValidateQuantityRange(cartItemModel.Quantity, product, out validationResult);
                if (validationResult != null)
                    return validationResult;

                
                //should we check for availability
                if (product.TrackInventory)
                {
                    if (!product.HasVariants)
                    {
                        ValidateProductQuantity(product, out validationResult);
                        if (validationResult != null)
                            return validationResult;
                    }
                    else
                    {
                        var attributeNames = cartItemModel.Attributes.Select(x => x.Name).ToList();
                        var attributeIds = product.ProductAttributes.Where(x => attributeNames.Contains(x.Label))
                            .Select(x => x.Id)
                            .ToList();

                        //if the product has variants only variants should be allowed to be added
                        var variants = _productVariantService.GetByProductId(product.Id);
                        variant = variants.FirstOrDefault(x =>
                        {
                            var variantAttributeIds = x.ProductVariantAttributes.Select(y => y.ProductAttributeId).ToList();
                            return !attributeIds.Except(variantAttributeIds).Any();
                        });
                        //is the variant available?
                        ValidateVariantQuantity(variant, out validationResult);
                        if (validationResult != null)
                            return validationResult;
                    }
                }

                var productAttributes = new Dictionary<string, IList<string>>();
                foreach (var cpa in cartItemModel.Attributes)
                {
                    productAttributes.TryAdd(cpa.Name, cpa.SelectedValues.Select(x => x.Name).ToList());
                }
                attributeJson = _dataSerializer.Serialize(productAttributes, false);
            }

            //guest signin if user is not signed in
            ApplicationEngine.GuestSignIn();
            currentUser = ApplicationEngine.CurrentUser;

            //find if there is already an existing product added
            var cart = cartItemModel.IsWishlist
                ? _cartService.GetWishlist(currentUser.Id)
                : _cartService.GetCart(currentUser.Id);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id && x.AttributeJson == attributeJson);

            if (cartItem == null)
            {
                //no issue adding this
                cartItem = new CartItem() {
                    ProductId = cartItemModel.ProductId,
                    Quantity = cartItemModel.Quantity,
                    AttributeJson = attributeJson,
                    ComparePrice = product.ComparePrice,
                    Price = product.Price,
                    ProductVariantId = variant?.Id ?? 0
                };
                if (cartItemModel.IsWishlist)
                {
                    _cartService.AddToWishlist(ApplicationEngine.CurrentUser.Id, cartItem);
                }
                else
                {
                    _cartService.AddToCart(ApplicationEngine.CurrentUser.Id, cartItem);
                }
            }
            else
            {
                //can we add this item to cart?
                ValidateQuantityRange(cartItem.Quantity + 1, product, out validationResult);
                if (validationResult != null)
                {
                    return validationResult;
                }
                //we can, increment and save
                cartItem.Quantity++;
                _cartService.UpdateCart(ApplicationEngine.CurrentUser.Id, cartItem);
            }
            return R.Success.Result;
        }

        [DualPost("update", Name = RouteNames.UpdateCart, OnlyApi = true)]
        public IActionResult UpdateCart(UpdateCartModel cartModel)
        {
            if (!cartModel.DiscountCoupon.IsNullEmptyOrWhiteSpace())
            {
                var discountStatus =
                    _cartService.SetDiscountCoupon(ApplicationEngine.CurrentUser.Id, cartModel.DiscountCoupon);
                if (discountStatus == DiscountApplicationStatus.Success)
                {
                    return R.Success.Result;
                }
                else
                {
                    return R.Fail.With("status", discountStatus).Result;
                }
            }
            else if (cartModel.RemoveCoupon)
            {
                _cartService.ClearDiscountCoupon(ApplicationEngine.CurrentUser.Id);
                return R.Success.Result;
            }
            if (!cartModel.GiftCode.IsNullEmptyOrWhiteSpace())
            {
                //do nothing
            }
            if (cartModel.CartItemId.HasValue && cartModel.Quantity.HasValue)
            {
                //get the cart
                var cart = cartModel.IsWishlist
                    ? _cartService.GetWishlist(ApplicationEngine.CurrentUser.Id)
                    : _cartService.GetCart(ApplicationEngine.CurrentUser.Id);
                var cartItem = cart.CartItems.FirstOrDefault(x => x.Id == cartModel.CartItemId.Value);
                if (cartItem == null)
                    return R.Fail.Result;

                //is it to remove it completely
                if (cartModel.Quantity.Value == 0)
                {
                    _cartItemService.Delete(cartItem);
                    return R.Success.Result;
                }

                var product = _productService.Get(cartItem.ProductId);
                if (product == null)
                    return R.Fail.Result;

                ValidateQuantityRange(cartModel.Quantity.Value, product, out IActionResult validationResult);
                //is the new quantity validated
                if (validationResult != null)
                    return validationResult;
                //are we tracking?
                if (product.TrackInventory)
                {
                    if (!product.HasVariants)
                    {
                        //this shouldn't be hit 
                        ValidateProductQuantity(product, out validationResult);
                    }
                    else
                    {
                        //get the variant
                        var variant = _productVariantService.Get(cartItem.ProductVariantId);
                        ValidateVariantQuantity(variant, out validationResult);
                    }
                    if (validationResult != null)
                        return validationResult;
                }
                cartItem.Quantity = cartModel.Quantity.Value;
                _cartService.UpdateCart(cart.UserId, cartItem);
                return R.Success.Result;
            }

            return R.Fail.Result;
        }

        [DualGet("~/account/wishlist", Name = RouteNames.AccountWishlist)]
        public IActionResult WishList()
        {
            var wishList = new CartObject(true).GetObject();
            return R.Success.With("wishlist", wishList).Result;
        }

        #region Helpers
        private void ValidateQuantityRange(int quantity, Product product, out IActionResult result)
        {
            result = null;
            //check for minimum order quantity
            if (quantity < product.MinimumPurchaseQuantity)
            {
                result = R.Fail.With("error", T("Minimum {0} item(s) must be ordered", arguments: product.MinimumPurchaseQuantity)).Result;
            }

            //check for maximum order quantity
            else if (quantity > product.MaximumPurchaseQuantity)
            {
                result = R.Fail.With("error", T("Maximum {0} item(s) can be ordered", arguments: product.MaximumPurchaseQuantity)).Result;
            }

            else if (product.TrackInventory && quantity > product.StockQuantity)
            {
                result = R.Fail.With("error", T("Only {0} item(s) are available", arguments: product.StockQuantity)).Result;
            }
        }

        private void ValidateVariantQuantity(ProductVariant variant, out IActionResult result)
        {
            result = null;
            if (variant == null)
            {
                result = R.Fail.With("error", T("The item is not available")).Result;
                return;
            }
            if (variant.StockQuantity == 0)
                result = R.Fail.With("error", T("The item is out of stock")).Result;
        }

        private void ValidateProductQuantity(Product product, out IActionResult result)
        {
            result = null;
            if (product.StockQuantity == 0)
                result = R.Fail.With("error", T("The item is out of stock")).Result;
        }
        #endregion
    }
}