using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Helpers;
using RoastedMarketplace.Services.Promotions;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Serializers;
using RoastedMarketplace.Services.Taxes;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Products
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

        public PriceAccountant(IDiscountCouponService discountCouponService, IUserService userService, ICartItemService cartItemService, IProductService productService, ITaxAccountant taxAccountant, TaxSettings taxSettings, ICartService cartService)
        {
            _discountCouponService = discountCouponService;
            _userService = userService;
            _cartItemService = cartItemService;
            _productService = productService;
            _taxAccountant = taxAccountant;
            _taxSettings = taxSettings;
            _cartService = cartService;
        }

        public DiscountApplicationStatus ApplyDiscountCoupon(string couponCode, Cart cart)
        {
            if (couponCode.IsNullEmptyOrWhitespace())
            {
                return DiscountApplicationStatus.InvalidCode;
            }

            //first get the coupon
            var discountCoupon = _discountCouponService.GetByCouponCode(couponCode);
            if (discountCoupon == null || !discountCoupon.Enabled)
                return DiscountApplicationStatus.InvalidCode;
            if (discountCoupon.Expired)
                return DiscountApplicationStatus.Expired;

            //first the dates
            if (discountCoupon.StartDate > DateTime.UtcNow)
                return DiscountApplicationStatus.InvalidCode;
            if (discountCoupon.EndDate.HasValue && discountCoupon.EndDate < DateTime.UtcNow)
                return DiscountApplicationStatus.Expired;

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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (cartUpdated)
            {
                _cartService.Update(cart);
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

        public decimal GetAutoDiscountedPriceForUser(Product product, User user, ref IList<DiscountCoupon> discountCoupons)
        {
            //get active discount coupons which don't have any code
            discountCoupons = discountCoupons ?? _discountCouponService.Get(x => x.Enabled && !x.Expired && !x.HasCouponCode).ToList();
            var discount = decimal.Zero;
            foreach (var dc in discountCoupons)
            {
                var currentDiscount = GetProductDiscountedPrice(dc, product, user);
                if (currentDiscount > discount)
                    discount = currentDiscount;
            }
            return product.Price - discount;
        }

        public void GetProductPriceDetails(Product product, Address address, out decimal price, out decimal tax)
        {
            var taxRate = _taxAccountant.GetFinalTaxRate(product, address);
            if (_taxSettings.PricesIncludeTax)
            {
                price = (product.Price * 100) / (taxRate + 100);
                tax = product.Price - price;
            }
            else
            {
                tax = product.Price * taxRate / 100;
                price = product.Price + tax;
            }
        }

        #region Helpers

        private decimal GetProductDiscountedPrice(DiscountCoupon discountCoupon, Product product, User user)
        {
            switch (discountCoupon.RestrictionType)
            {
                case RestrictionType.Products:
                    return discountCoupon.RestrictionIds().Contains(product.Id) ? discountCoupon.GetDiscountAmount(product.Price) : 0;
                case RestrictionType.Categories:
                    var categoryIds = discountCoupon.RestrictionIds().ToArray();
                    var categoryProductIds = _productService.GetProductIdsByCategoryIds(categoryIds);
                    return categoryProductIds.Contains(product.Id) ? discountCoupon.GetDiscountAmount(product.Price) : 0;
                case RestrictionType.Users:
                    return discountCoupon.RestrictionIds().Contains(user.Id) ? discountCoupon.GetDiscountAmount(product.Price) : 0;
                case RestrictionType.UserGroups:
                    return 0;
                case RestrictionType.Roles:
                    var roleIds = discountCoupon.RestrictionIds();
                    return user.Roles.Any(x => roleIds.Contains(x.Id)) ? discountCoupon.GetDiscountAmount(product.Price) : 0;
                case RestrictionType.Vendors:
                    var vendorIds = discountCoupon.RestrictionIds().ToArray();
                    var vendorProductIds = _productService.GetProductIdsByVendorIds(vendorIds);
                    return vendorProductIds.Contains(product.Id) ? discountCoupon.GetDiscountAmount(product.Price) : 0;
                case RestrictionType.Manufacturers:
                    return product.ManufacturerId.HasValue &&
                           discountCoupon.RestrictionIds().Contains(product.ManufacturerId.Value)
                        ? discountCoupon.GetDiscountAmount(product.Price)
                        : 0;
                case RestrictionType.PaymentMethods:
                case RestrictionType.ShippingMethods:
                case RestrictionType.OrderTotal:
                case RestrictionType.OrderSubTotal:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException();
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
                cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
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
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
                    cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
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
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
                    cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
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
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
                    cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
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
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
                    cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
                    cartUpdated = true;
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
                    cartItem.Discount = discountCoupon.GetDiscountAmount(cartItem.Price);
                    cartItem.FinalPrice = cartItem.Price - cartItem.Discount;
                    cartItemUpdated = true;
                }
            }
            return cartItemUpdated;
        }

        private bool ApplyPaymentMethodDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            var paymentMethodNames = discountCoupon.RestrictionValues();
            if (paymentMethodNames.Contains(cart.PaymentMethodName))
            {
                var paymentHandler = ModuleHelper.GetPaymentHandler(cart.PaymentMethodName);
                var discount = discountCoupon.GetDiscountAmount(paymentHandler.GetPaymentHandlerFee(cart));
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
                var shippingHandler = ModuleHelper.GetShipmentHandler(cart.ShippingMethodName);
                var discount = discountCoupon.GetDiscountAmount(shippingHandler.GetShippingHandlerFee(cart));
                cart.ShippingFee = cart.ShippingFee - discount;
                return true;
            }
            return false;
        }

        private bool ApplyOrderTotalDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            IList<DiscountCoupon> discountCoupons = null;
            var orderTotalForDiscount = decimal.Zero;
            var otherOrderTotal = decimal.Zero;
            foreach (var cartItem in cart.CartItems)
            {
                var discountedPrice = GetAutoDiscountedPriceForUser(cartItem.Product, cart.User, ref discountCoupons);
                if (discountedPrice < cartItem.Price)
                {
                    if (discountCoupon.ExcludeAlreadyDiscountedProducts)
                    {
                        otherOrderTotal += discountedPrice;
                        continue; //exclude this product
                    }
                    orderTotalForDiscount += discountedPrice;
                }
                else
                {
                    orderTotalForDiscount += cartItem.Price;
                }
            }
            cart.FinalAmount = orderTotalForDiscount + otherOrderTotal;
            return true;
        }

        private bool ApplyOrderSubTotalDiscount(DiscountCoupon discountCoupon, Cart cart)
        {
            //todo: implement this
            return true;
        }
        #endregion


    }
}