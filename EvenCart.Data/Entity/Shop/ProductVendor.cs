using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductVendor : FoundationEntity
    {
        public int ProductId { get; set; }
        
        public int VendorId { get; set; }

        public decimal Price { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}