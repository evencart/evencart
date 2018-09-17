using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class SpecificationValue : FoundationEntity
    {
        public int SpecificationId { get; set; }

        public string Value { get; set; }
    }
}