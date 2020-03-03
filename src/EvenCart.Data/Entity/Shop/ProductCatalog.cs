using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductCatalog : FoundationEntity
    {
        public int ProductId { get; set; }

        public int CatalogId { get; set; }
    }
}