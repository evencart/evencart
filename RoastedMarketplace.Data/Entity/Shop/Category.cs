using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class Category : FoundationEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public int? TaxId { get; set; }
    }
}