using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Purchases
{
    public class Shipment : FoundationEntity
    {
        public string TrackingNumber { get; set; }

        public string Remarks { get; set; }

        public string ShippingMethodName { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public int WarehouseId { get; set; }

        #region Virtual Properties
        public virtual IList<ShipmentItem> ShipmentItems { get; set; }

        public virtual IList<ShipmentHistory> ShipmentStatusHistories { get; set; }

        public virtual User User { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        #endregion
    }
}