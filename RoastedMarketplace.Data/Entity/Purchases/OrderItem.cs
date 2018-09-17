using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Purchases
{
    public class OrderItem : FoundationEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string AttributeJson { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        #region Virtual Properties
        public virtual Order Order { get; set; }

        public virtual Shipment Shipment { get; set; }
        #endregion
    }
}