using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Promotions;

namespace EvenCart.Services.Promotions
{
    public interface IDiscountCouponService : IFoundationEntityService<DiscountCoupon>
    {
        DiscountCoupon GetByCouponCode(string couponCode);

        IEnumerable<DiscountCoupon> SearchDiscountCoupons(string searchText, out int totalMatches, int page = 1, int count = 15);

        void SetRestrictionIdentifiers(int discountCouponId, IList<string> restrictionIdentifiers);
    }
}