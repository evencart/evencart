using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class ProductSpecificationGroup : FoundationEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public int ProductId { get; set; }
    }
}