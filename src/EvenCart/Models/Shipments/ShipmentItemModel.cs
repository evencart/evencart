using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Shipments
{
    public class ShipmentItemModel : FoundationModel
    {
        public int OrderItemId { get; set; }

        public string ProductName { get; set; }

        public string SeName { get; set; }

        public string AttributeText { get; set; }

        public int OrderedQuantity { get; set; }

        public int ShippedQuantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}