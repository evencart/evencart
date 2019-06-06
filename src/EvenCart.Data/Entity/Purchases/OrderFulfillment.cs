using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Entity.Purchases
{
    public class OrderFulfillment : FoundationEntity
    {
        public int OrderId { get; set; }

        public int OrderItemId { get; set; }

        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public bool Verified { get; set; }

        #region Virtual Properties
        public virtual Order Order { get; set; }

        public virtual OrderItem OrderItem { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual WarehouseInventory WarehouseInventory { get; set; }
        #endregion
    }
}