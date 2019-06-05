using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class WarehouseInventory : FoundationEntity
    {
        public int ProductId { get; set; }

        public int? ProductVariantId { get; set; }

        public int WarehouseId { get; set; }

        public int TotalQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        #region Virtual Properties

        public virtual int AvailableQuantity => TotalQuantity - ReservedQuantity;

        public virtual Product Product { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
        #endregion
    }
}