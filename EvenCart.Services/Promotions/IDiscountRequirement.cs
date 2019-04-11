using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Data.Entity.Purchases;

namespace EvenCart.Services.Promotions
{
    public interface IDiscountRequirement : IPlugin
    {
        bool CanDiscountBeApplied(DiscountCoupon coupon, Cart cart);
    }
}