using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductVariant : FoundationEntity
    {
        public int ProductId { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public decimal? Price { get; set; }

        public decimal? ComparePrice { get; set; }

        public bool TrackInventory { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public int MediaId { get; set; }

        #region Virtual Properties
        public virtual IList<ProductVariantAttribute> ProductVariantAttributes { get; set; }

        public virtual IList<WarehouseInventory> Inventories { get; set; }
        #endregion
    }
}