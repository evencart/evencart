using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Promotions;

namespace RoastedMarketplace.Services.Promotions
{
    public interface IDiscountCouponService : IFoundationEntityService<DiscountCoupon>
    {
        DiscountCoupon GetByCouponCode(string couponCode);
    }
}