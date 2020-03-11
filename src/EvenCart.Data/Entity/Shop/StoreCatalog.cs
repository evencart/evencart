using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class StoreCatalog : FoundationEntity
    {
        public int StoreId { get; set; }

        public int CatalogId { get; set; }
    }
}