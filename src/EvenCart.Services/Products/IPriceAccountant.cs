using System.Collections.Generic;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Promotions;

namespace EvenCart.Services.Products
{
    public interface IPriceAccountant
    {
        DiscountApplicationStatus ApplyDiscountCoupon(string couponCode, Cart cart);

        DiscountApplicationStatus ApplyDiscountCoupon(DiscountCoupon coupon, Cart cart);

        void ClearCouponCode(Cart cart);

        void RefreshCartParameters(Cart cart);

        decimal GetAutoDiscountedPriceForUser(Product product, ProductVariant variant, User user, int quantity, ref IList<DiscountCoupon> discountCoupons, out decimal discount);

        void GetProductPriceDetails(Product product, Address address, decimal? basePrice, out decimal price, out decimal tax, out decimal taxRate, out string taxName);

        decimal ConvertCurrency(decimal input, Currency targetCurrency, Rounding? roundingType = null);
    }
}