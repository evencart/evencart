using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Services.Promotions;

namespace RoastedMarketplace.Services.Products
{
    public interface IPriceAccountant
    {
        DiscountApplicationStatus ApplyDiscountCoupon(string couponCode, Cart cart);

        DiscountApplicationStatus ApplyDiscountCoupon(DiscountCoupon coupon, Cart cart);

        void ClearCouponCode(Cart cart);

        void RefreshCartParameters(Cart cart);

        decimal GetAutoDiscountedPriceForUser(Product product, User user, ref IList<DiscountCoupon> discountCoupons, out decimal discount);

        void GetProductPriceDetails(Product product, Address address, decimal? basePrice, out decimal price, out decimal tax, out decimal taxRate);
    }
}