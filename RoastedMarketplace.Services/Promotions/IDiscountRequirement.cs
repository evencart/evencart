using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Promotions
{
    public interface IDiscountRequirement : IModule
    {
        bool CanDiscountBeApplied(DiscountCoupon coupon, Cart cart);
    }
}