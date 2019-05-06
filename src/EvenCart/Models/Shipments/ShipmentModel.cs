using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Shipments
{
    public class ShipmentModel : FoundationEntityModel
    {
        public int OrderId { get; set; }

        public string TrackingNumber { get; set; }

        public string Remarks { get; set; }

        public string ShippingMethodName { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public IList<ShipmentItemModel> ShipmentItems { get; set; }
    }
}