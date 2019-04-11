using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Promotions
{
    public class RestrictionValue : FoundationEntity
    {
        public int DiscountCouponId { get; set; }

        public string RestrictionIdentifier { get; set; }
    }
}