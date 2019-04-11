using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductSpecificationGroup : FoundationEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public int ProductId { get; set; }
    }
}