using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class ProductSpecification : FoundationEntity
    {
        public int ProductId { get; set; }

        public int SpecificationAttributeId { get; set; }

        public int SpecificationValueId { get; set; }

        public string CustomValue { get; set; }
    }
}