using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Purchases
{
    public class ShipmentItem : FoundationEntity
    {
        public int ShipmentId { get; set; }

        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        #region Virtual Properties
        public virtual OrderItem OrderItem { get; set; }

        public virtual Shipment Shipment { get; set; }
        #endregion

    }
}