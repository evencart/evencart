using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Formatter;
using EvenCart.Services.Helpers;
using EvenCart.Services.Promotions;
using EvenCart.Services.Purchases;
using EvenCart.Services.Taxes;
using EvenCart.Services.Users;

namespace EvenCart.Services.Products
{
    public class PriceAccountant : IPriceAccountant
    {
        private readonly IDiscountCouponService _discountCouponService;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;
        private readonly ITaxAccountant _taxAccountant;
        private readonly TaxSettings _taxSettings;
        private readonly ICartService _cartService;
        private readonly IProductVariantService _productVariantService;
        private readonly IRoundingService _roundingService;
        private readonly IOrderService _orderService;
        public PriceAccountant(IDiscountCouponService discountCouponService, IUserService userService, ICartItemService cartItemService, IProductService productService, ITaxAccountant taxAccountant, TaxSettings taxSettings, ICartService cartService, IProductVariantService productVariantService, IRoundingService roundingService, IOrderService orderService)
        {
            _discountCouponService = discountCouponService;
            _userService = userService;
            _cartItemService = cartItemService;
            _productService = productService;
            _taxAccountant = taxAccountant;
            _taxSettings = taxSettings;
            _cartService = cartService;
            _productVariantService = productVariantService;
            _roundingService = roundingService;
            _orderService = orderService;
        }

        public DiscountApplicationStatus ApplyDiscountCoupon(string couponCode, Cart cart)
        {
            if (couponCode.IsNullEmptyOrWhiteSpace())
            {
                return DiscountApplicationStatus.InvalidCode;
            }

            //first get the coupon
            var discountCoupon = _discountCouponService.GetByCouponCode(couponCode);
            return ApplyDiscountCoupon(discountCoupon, cart);
        }

        public DiscountApplicationStatus ApplyDiscountCoupon(DiscountCoupon discountCoupon, Cart cart)
        {
            if (!CanApplyDiscount(discountCoupon, cart.UserId, out var status))
            {
                return status;
            }
          
            //is there a min. total required
            if (discountCoupon.MinimumOrderSubTotal > 0 && discountCoupon.MinimumOrderSubTotal > GetOrderSubTotal(cart))
            {
                return DiscountApplicationStatus.NotEligibleForCart;
            }
      

            var cartItemUpdated = false;
            var cartUpdated = false;
            //check for restriction type
            switch (discountCoupon.RestrictionType)
            {
                case RestrictionType.Products:
                    cartItemUpdated = ApplyProductDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.Categories:
                    cartItemUpdated = ApplyCategoryDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.Users:
                    cartItemUpdated = ApplyUserDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.UserGroups:
                    cartItemUpdated = ApplyUserGroupDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.Roles:
                    cartItemUpdated = ApplyRoleDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.Vendors:
                    cartItemUpdated = ApplyVendorDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.Manufacturers:
                    cartItemUpdated = ApplyManufacturerDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.PaymentMethods:
                    cartUpdated = ApplyPaymentMethodDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.ShippingMethods:
                    cartUpdated = ApplyShippingMethodDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.OrderTotal:
                    cartUpdated = ApplyOrderTotalDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.OrderSubTotal:
                    cartUpdated = ApplyOrderSubTotalDiscount(discountCoupon, cart);
                    break;
                case RestrictionType.ShippingFee:
                    cartUpdated = ApplyShippingDiscount(discountCoupon, cart);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (cartUpdated || cartItemUpdated)
            {
                cart.DiscountCouponId = discountCoupon.Id;
                _cartService.Update(cart);
                if (cartUpdated)
                    return DiscountApplicationStatus.Success;
            }
            if (cartItemUpdated)
            {
                foreach (var cartItem in cart.CartItems)
                    _cartItemService.Update(cartItem);

                return DiscountApplicationStatus.Success;
            }
            return DiscountApplicationStatus.NotEligibleForCart;
        }

        public bool CanApplyDiscount(DiscountCoupon discountCoupon, int userId, out DiscountApplicationStatus status)
        {
            if (discountCoupon == null || !discountCoupon.Enabled)
            {
                status = DiscountApplicationStatus.InvalidCode;
                return false;
            }
            if (discountCoupon.Expired)
            {
                status = DiscountApplicationStatus.Expired;
                return false;
            }

            //first the dates
            if (discountCoupon.StartDate > DateTime.UtcNow)
            {
                status = DiscountApplicationStatus.InvalidCode;
                return false;
            }

            if (discountCoupon.EndDate.HasValue && discountCoupon.EndDate < DateTime.UtcNow)
            {
                status = DiscountApplicationStatus.Expired;
                return false;
            }
            //number of usages
            if (discountCoupon.TotalNumberOfTimes > 0)
            {
                var orderCount = _orderService.Count(x =>
                    x.DiscountId == discountCoupon.Id && x.PaymentStatus == PaymentStatus.Complete);
                if (orderCount >= discountCoupon.TotalNumberOfTimes)
                {
                    status = DiscountApplicationStatus.Exhausted;
                    return false;
                }
            }
            if (discountCoupon.NumberOfTimesPerUser > 0)
            {
                var orderCount = _orderService.Count(x =>
                    x.DiscountId == discountCoupon.Id && x.PaymentStatus == PaymentStatus.Complete && x.UserId == userId);
                if (orderCount >= discountCoupon.NumberOfTimesPerUser)
                {
                    status = DiscountApplicationStatus.Exhausted;
                    return false;
                }
            }

            status = DiscountApplicationStatus.Success;
            return true;
        }

        public bool CanApplyDiscount(string couponCode, int userId, out DiscountApplicationStatus status)
        {
            if (couponCode.IsNullEmptyOrWhiteSpace())
            {
                status = DiscountApplicationStatus.InvalidCode;
                return false;
            }

            //first get the coupon
            var discountCoupon = _discountCouponService.GetByCouponCode(couponCode);
            return CanApplyDiscount(discountCoupon, userId, out status);
        }

        public bool CanApplyDiscount(int couponCodeId, int userId, out DiscountApplicationStatus status)
        {
            var discountCoupon = _discountCouponService.FirstOrDefault(x => x.Id == couponCodeId);
            return CanApplyDiscount(discountCoupon, userId, out status);
        }

        public void ClearCouponCode(Cart cart)
        {
            cart.DiscountCouponId = 0;
            cart.DiscountCoupon = null;
            cart.Discount = 0;
            _cartService.Update(cart);
            //refresh pricing
            RefreshCartParameters(cart);
        }

        public void RefreshCartParameters(Cart cart)
        {
            //update prices if we need to
            var cartProductIds = cart.CartItems.Select(x => x.ProductId).ToList();
            if (cartProductIds.Any())
            {
                var products = _productService.GetProducts(cartProductIds);
                var productVariants = _productVariantService.Get(x => cartProductIds.Contains(x.ProductId)).ToList();

                Transaction.Initiate(transaction =>
                {
                    //preserve autodiscounts for performance
                    IList<DiscountCoupon> discountCoupons = _discountCouponService.Get(x => x.Enabled && !x.HasCouponCode).Where(x => !x.Expired).ToList();
                    //update cart items if required
                    foreach (var ci in cart.CartItems)
                    {
                        var product = products.FirstOrDefault(x => x.Id == ci.ProductId);
                        if (product == null)
                        {
                            //remove from cart because we can't find the product
                            _cartService.RemoveFromCart(ci.Id, transaction);
                        }
                        else
                        {
                            if (!product.Published || product.Deleted)
                            {
                                //remove from cart because product shouldn't be visible
                                _cartService.RemoveFromCart(ci.Id, transaction);
                            }
                            else if (product.RestrictedToRoles)
                            {
                                var roleIds = cart.User.Roles.Select(x => x.Id).ToList();
                                if (product.EntityRoles.All(x => !roleIds.Contains(x.RoleId)))
                                {
                                    //remove because role is not allowed to buy this product
                                    _cartService.RemoveFromCart(ci.Id, transaction);
                                }
                            }
                            else if (product.TrackInventory)
                            {
                                if (!product.HasVariants && !product.IsAvailableInStock())
                                {
                                    //remove from cart because we can't find the product
                                    _cartService.RemoveFromCart(ci.Id, transaction);
                                }
                            }
                            if (ci.Quantity < product.MinimumPurchaseQuantity && product.MinimumPurchaseQuantity > 0)
                            {
                                //is there a difference in quantity that's required
                                ci.Quantity = product.MinimumPurchaseQuantity;
                            }
                            if (ci.Quantity > product.MaximumPurchaseQuantity && product.MaximumPurchaseQuantity > 0)
                            {
                                //is there a difference in quantity that's required
                                ci.Quantity = product.MaximumPurchaseQuantity;
                            }

                            var variant = product.HasVariants && ci.ProductVariantId > 0
                                ? productVariants.FirstOrDefault(x => x.Id == ci.ProductVariantId)
                                : null;
                            //are there any discounted price for product
                            var basePrice =  GetAutoDiscountedPriceForUser(product, variant, cart.User, ci.Quantity, ref discountCoupons, out decimal discount);
                            if (product.HasVariants && ci.ProductVariantId > 0)
                            {
                                if (variant == null || (variant.TrackInventory && !variant.IsAvailableInStock(product)))
                                    //remove from cart because we can't find the variant or it's out of stock
                                    _cartService.RemoveFromCart(ci.Id, transaction);
                                else
                                {
                                    var comparisonPrice = variant.ComparePrice ?? product.ComparePrice;
                                    var price = Math.Min(basePrice, variant.Price ?? product.Price);

                                    GetProductPriceDetails(product, cart.BillingAddress, price, out decimal priceWithoutTax, out decimal tax, out decimal taxRate, out var taxName);

                                    //do we need an update?
                                    if (priceWithoutTax != ci.Price || comparisonPrice != ci.ComparePrice || tax != ci.Tax || taxRate != ci.TaxPercent || ci.FinalPrice == 0 || ci.TaxName != taxName)
                                    {
                                        ci.Price = priceWithoutTax;
                                        ci.ComparePrice = comparisonPrice;
                                        ci.Tax = tax * ci.Quantity;
                                        ci.TaxPercent = taxRate;
                                        ci.Discount = discount;
                                        ci.FinalPrice = ci.Price * ci.Quantity + ci.Tax;
                                        ci.TaxName = taxName;
                                        _cartItemService.Update(ci);
                                    }
                                }
                            }
                            else
                            {
                                GetProductPriceDetails(product, cart.BillingAddress, basePrice, out decimal priceWithoutTax, out decimal tax, out decimal taxRate, out var taxName);
                                tax = tax * ci.Quantity;
                                //do we need an update?
                                if (priceWithoutTax != ci.Price || product.ComparePrice != ci.ComparePrice || tax != ci.Tax || taxRate != ci.TaxPercent || ci.FinalPrice == 0 || ci.TaxName != taxName)
                                {
                                    ci.Price = priceWithoutTax;
                                    ci.ComparePrice = product.ComparePrice;
                                    ci.Tax = tax;
                                    ci.TaxPercent = taxRate;
                                    ci.Discount = discount;
                                    ci.FinalPrice = ci.Price * ci.Quantity + ci.Tax;
                                    ci.TaxName = taxName;
                                    _cartItemService.Update(ci);
                                }
                            }

                        }
                    }
                    cart.FinalAmount = cart.CartItems.Sum(x => x.FinalPrice);
                    cart.CompareFinalAmount = cart.CartItems.Sum(x => x.ComparePrice ?? 0);

                    //do we have a discount coupon
                    if (cart.DiscountCoupon != null && cart.DiscountCoupon.Enabled && !cart.DiscountCoupon.Expired)
                    {
                        if (ApplyDiscountCoupon(cart.DiscountCoupon, cart) == DiscountApplicationStatus.Success)
                        {
                            //cart already updated so return
                            return;
                        }
                    }
                    else
                    {
                        if (cart.DiscountCoupon != null)
                        {
                            cart.DiscountCouponId = 0;
                            _cartService.Update(cart);
                        }
                      
                        ////find coupons which should be automatically applied
                        foreach(var dc in discountCoupons)
                            if (ApplyDiscountCoupon(dc, cart) == DiscountApplicationStatus.Success)
                                break;
                    }

                });
            }
        }

        public decimal GetAutoDiscountedPriceForUser(Product product, ProductVariant variant, User user, int quantity, ref IList<DiscountCoupon> discountCoupons, out decimal discount)
        {
            //get active discount coupons which don't have any code
            discountCoupons = discountCoupons ?? _discountCouponService.Get(x => x.Enabled && !x.HasCouponCode).Where(x => !x.Expired).ToList();
            discount = decimal.Zero;
            foreach (var dc in discountCoupons)
            {
                var currentDiscount = GetProductDiscountedPrice(dc, product, variant, user, quantity);
                if (currentDiscount > discount)
                    discount = currentDiscount;
            }
            return (variant?.Price ?? product.Price) - discount;
        }

        public void GetProductPriceDetails(Product product, Address address, decimal? basePrice, out decimal price, out decimal tax, out decimal taxRate, out string taxName)
        {
            taxRate = _taxAccountant.GetFinalTaxRate(product, address, out taxName);
            var fromBasePrice = basePrice ?? product.Price;
            if (_taxSettings.PricesIncludeTax)
            {
                price = (fromBasePrice * 100) / (taxRate + 100);
                tax = fromBasePrice - price;
            }
            else
            {
                tax = fromBasePrice * taxRate / 100;
                price = fromBasePrice;
            }
        }

        public decimal ConvertCurrency(decimal input, Currency targetCurrency, Rounding? roundingType = null)
        {
            if (targetCurrency.ExchangeRate > 0)
            {
                input = input * targetCurrency.ExchangeRate;
                input = _roundingService.Round(input, targetCurrency.NumberOfDecimalPlaces, roundingType ?? targetCurrency.RoundingType);
            }
            return input;
        }

        #region Helpers

        private decimal GetProductDiscountedPrice(DiscountCoupon discountCoupon, Product product, ProductVariant variant, User user, int quantity)
        {
            var price = variant?.Price ?? product.Price;
            switch (discountCoupon.RestrictionType)
            {
                case RestrictionType.Products:
                    return discountCoupon.RestrictionIds().Contains(product.Id) ? discountCoupon.GetDiscountAmount(price, quantity) : 0;
                case RestrictionType.Categories:
                    var categoryIds = discountCoupon.RestrictionIds().ToArray();
                    var categoryProductIds = _productService.GetProductIdsByCategoryIds(categoryIds);
                    return categoryProductIds.Contains(product.Id) ? discountCoupon.GetDiscountAmount(price, quantity) : 0;
                case RestrictionType.Users:
                    return discountCoupon.RestrictionIds().Contains(user.Id) ? discountCoupon.GetDiscountAmount(price, quantity) : 0;
                case RestrictionType.UserGroups:
                    return 0;
                case RestrictionType.Roles:
                    var roleIds = discountCoupon.RestrictionIds();
                    return user.Roles.Any(x => roleIds.Contains(x.Id)) ? discountCoupon.GetDiscountAmount(price, quantity) : 0;
                case RestrictionType.Vendors:
                    var vendorIds = discountCoupon.RestrictionIds().ToArray();
                    var vendorProductIds = _productService.GetProductIdsByVendorIds(vendorIds);
                    return vendorProductIds.Contains(product.Id) ? discountCoupon.GetDiscountAmount(price, quantity) : 0;
                case RestrictionType.Manufacturers:
                    return product.ManufacturerId.HasValue &&
                           discountCoupon.RestrictionIds().Contains(product.ManufacturerId.Value)
                        ? discountCoupon.GetDiscountAmount(price, quantity)
                        : 0;
                default:
                    return 0;
            }
        }

        private bool ApplyProductDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var productIds = discountCoupon.RestrictionIds();
            var cartItemUpdated = false;
            foreach (var cartItem in cart.CartItems)
            {
                if (!productIds.Contains(cartItem.ProductId))
                    continue;
                cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
                cartItemUpdated = true;
            }
            return cartItemUpdated;
        }

        private bool ApplyCategoryDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var categoryIds = discountCoupon.RestrictionIds().ToArray();
            var categoryProductIds = _productService.GetProductIdsByCategoryIds(categoryIds);
            var cartUpdated = false;
            foreach (var cartItem in cart.CartItems)
            {
                if (categoryProductIds.Contains(cartItem.ProductId))
                {
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                    cartUpdated = true;
                }
            }
            return cartUpdated;
        }

        private bool ApplyUserDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var userIds = discountCoupon.RestrictionIds();
            if (userIds.Contains(cart.UserId))
            {
                foreach (var cartItem in cart.CartItems)
                {
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                }
                if (discountCoupon.MaximumDiscountAmount > 0 &&
                    cart.CartItems.Sum(x => x.Discount) > discountCoupon.MaximumDiscountAmount)
                {
                    cart.Discount = discountCoupon.MaximumDiscountAmount;
                    foreach (var cartItem in cart.CartItems)
                    {
                        cartItem.Discount = 0;
                    }
                }

                return true;
            }
            return false;
        }

        private bool ApplyUserGroupDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            //todo: implement this
            return true;
        }

        private bool ApplyRoleDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var roleIds = discountCoupon.RestrictionIds();
            var user = _userService.Get(cart.UserId);
            if (user.Roles.Any(x => roleIds.Contains(x.Id)))
            {
                foreach (var cartItem in cart.CartItems)
                {
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                }

                if (discountCoupon.MaximumDiscountAmount > 0 &&
                    cart.CartItems.Sum(x => x.Discount) > discountCoupon.MaximumDiscountAmount)
                {
                    cart.Discount = discountCoupon.MaximumDiscountAmount;
                    foreach (var cartItem in cart.CartItems)
                    {
                        cartItem.Discount = 0;
                    }
                }
                return true;
            }
            return false;
        }

        private bool ApplyVendorDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var vendorIds = discountCoupon.RestrictionIds().ToArray();
            var vendorProductIds = _productService.GetProductIdsByVendorIds(vendorIds);
            var cartUpdated = false;
            foreach (var cartItem in cart.CartItems)
            {
                if (vendorProductIds.Contains(cartItem.ProductId))
                {
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                    cartUpdated = true;
                }
            }

            if (cartUpdated && discountCoupon.MaximumDiscountAmount > 0 &&
                cart.CartItems.Sum(x => x.Discount) > discountCoupon.MaximumDiscountAmount)
            {
                cart.Discount = discountCoupon.MaximumDiscountAmount;
                foreach (var cartItem in cart.CartItems)
                {
                    cartItem.Discount = 0;
                }
            }
            return cartUpdated;
        }

        private bool ApplyManufacturerDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var manufacturerIds = discountCoupon.RestrictionIds();
            var cartItemUpdated = false;
            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Product.ManufacturerId.HasValue &&
                    manufacturerIds.Contains(cartItem.Product.ManufacturerId.Value))
                {
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price, cartItem.Quantity);
                    cartItemUpdated = true;
                }
            }
            if (cartItemUpdated && discountCoupon.MaximumDiscountAmount > 0 &&
                cart.CartItems.Sum(x => x.Discount) > discountCoupon.MaximumDiscountAmount)
            {
                cart.Discount = discountCoupon.MaximumDiscountAmount;
                foreach (var cartItem in cart.CartItems)
                {
                    cartItem.Discount = 0;
                }
            }
            return cartItemUpdated;
        }

        private bool ApplyPaymentMethodDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var paymentMethodNames = discountCoupon.RestrictionValues();
            if (paymentMethodNames.Contains(cart.PaymentMethodName))
            {
                var paymentHandler = PluginHelper.GetPaymentHandler(cart.PaymentMethodName);
                var discount = discountCoupon.GetDiscountAmount(paymentHandler.GetPaymentHandlerFee(cart), 1);
                if (discountCoupon.HasCouponCode)
                    cart.Discount = discount;
                cart.PaymentMethodFee = cart.PaymentMethodFee - discount;
                return true;
            }
            return false;
        }

        private bool ApplyShippingMethodDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var shippingMethodNames = discountCoupon.RestrictionValues();
            if (shippingMethodNames.Contains(cart.ShippingMethodName))
            {
                return ApplyShippingDiscount(discountCoupon, cart);
            }
            return false;
        }

        private bool ApplyOrderTotalDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var orderTotalForDiscount = decimal.Zero;
            var otherOrderTotal = decimal.Zero;
            foreach (var cartItem in cart.CartItems)
            {
                if (discountCoupon.ExcludeAlreadyDiscountedProducts && cartItem.Discount > 0)
                {
                    otherOrderTotal += cartItem.Tax + cartItem.Price * cartItem.Quantity;
                    continue;
                }
                orderTotalForDiscount += cartItem.Tax + cartItem.Price * cartItem.Quantity;
            }

            cart.Discount = discountCoupon.GetDiscountAmount(orderTotalForDiscount, 1);
            return true;
        }

        private bool ApplyOrderSubTotalDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var subTotal = GetOrderSubTotal(cart);
            cart.Discount = discountCoupon.GetDiscountAmount(subTotal, 1);
            return true;
        }

        private bool ApplyShippingDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var discount = discountCoupon.GetDiscountAmount(cart.ShippingFee, 1);
            cart.Discount = discount;
            return true;
        }

        private decimal GetOrderSubTotal(Cart cart)
        {
            return cart.CartItems.Sum(x => x.Price * x.Quantity);
        }
        #endregion


    }
}