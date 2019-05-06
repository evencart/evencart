using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class OrderItemModel : FoundationEntityModel
    {
        public int ProductId { get; set; }

        public string ImageUrl { get; set; }

        public string ProductName { get; set; }

        public string SeName { get; set; }

        public string AttributeText { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }
    }
}