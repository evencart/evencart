using System.Linq;
using DotEntity.Enumerations;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Promotions;

namespace RoastedMarketplace.Services.Purchases
{
    public class CartService : FoundationEntityService<Cart>, ICartService
    {
        private readonly ICartItemService _cartItemService;
        private readonly IPriceAccountant _priceAccountant;
        public CartService(ICartItemService cartItemService, IPriceAccountant priceAccountant)
        {
            _cartItemService = cartItemService;
            _priceAccountant = priceAccountant;
        }

        public Cart GetCart(int userId)
        {
            return GetCart(userId, false);
        }

        public Cart GetWishlist(int userId)
        {
            return GetCart(userId, true);
        }

        private Cart GetCart(int userId, bool isWishlist)
        {
            var userCart = Repository.Where(x => x.UserId == userId && x.IsWishlist == isWishlist)
                               .Join<CartItem>("Id", "CartId", joinType: JoinType.LeftOuter)
                               .Join<Product>("ProductId", "Id", joinType: JoinType.LeftOuter)
                               .Relate(RelationTypes.OneToMany<Cart, CartItem>())
                               .Relate<Product>((cart, product) =>
                               {
                                   var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
                                   if (cartItem != null)
                                       cartItem.Product = product;
                               })
                               .SelectNested()
                               .FirstOrDefault() ?? new Cart() {UserId = userId, IsWishlist = isWishlist};

            if (userCart.Id == 0)
            {
                //save a new cart for user
                userCart.UserId = userId;
                Insert(userCart);
            }
            return userCart;
        }

        public void AddToCart(int userId, CartItem item)
        {
            //get user's cart
            var userCart = GetCart(userId);
            //save the item now
            item.CartId = userCart.Id;
            _cartItemService.Insert(item);
        }

        public void AddToWishlist(int userId, CartItem item)
        {
            //get user's cart
            var userCart = GetWishlist(userId);
            //save the item now
            item.CartId = userCart.Id;
            _cartItemService.Insert(item);
        }

        public void RemoveFromCart(int cartItemId)
        {
            _cartItemService.Delete(x => x.Id == cartItemId);
        }

        public void ClearCart(int userId)
        {
            //clear user's cart
            ClearCart(userId, false);
        }

        public void ClearWishlist(int userId)
        {
            //clear wishlist
            ClearCart(userId, true);
        }

        public void SetPaymentMethodOnCart(int userId, string paymentMethodName, string paymentMethodData)
        {
            var cart = GetCart(userId);
            cart.PaymentMethodName = paymentMethodName;
            cart.PaymentMethodData = paymentMethodData;
            Update(cart);
        }

        public void SetShippingMethodOnCart(int userId, string shippingMethodName)
        {
            var cart = GetCart(userId);
            cart.ShippingMethodName = shippingMethodName;
            Update(cart);
        }

        public DiscountApplicationStatus SetDiscountCoupon(int userId, string discountCoupon)
        {
            var cart = GetCart(userId);
            return _priceAccountant.ApplyDiscountCoupon(discountCoupon, cart);
        }

        public void SetAddresses(int userId, int billingAddressId, int shippingAddressId)
        {
            var cart = GetCart(userId);
            cart.BillingAddressId = billingAddressId;
            cart.ShippingAddressId = shippingAddressId;
            Update(cart);
        }

        private void ClearCart(int userId, bool isWishlist)
        {
            var cart = isWishlist ? GetWishlist(userId) : GetCart(userId);
            _cartItemService.Delete(x => x.CartId == cart.Id);
        }
    }
}