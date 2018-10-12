using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Promotions
{
    public class RestrictionValue : FoundationEntity
    {
        public int DiscountCouponId { get; set; }

        public string RestrictionIdentifier { get; set; }
    }
}