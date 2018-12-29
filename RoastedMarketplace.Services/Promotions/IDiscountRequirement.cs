using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Data.Entity.Purchases;

namespace RoastedMarketplace.Services.Promotions
{
    public interface IDiscountRequirement : IPlugin
    {
        bool CanDiscountBeApplied(DiscountCoupon coupon, Cart cart);
    }
}