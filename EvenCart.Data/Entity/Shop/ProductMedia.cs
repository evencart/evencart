using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductMedia : FoundationEntity
    {
        public int ProductId { get; set; }

        public int MediaId { get; set; }
    }
}