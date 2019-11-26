using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Entity.Purchases
{
    public class CartItem : FoundationEntity
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int ProductVariantId { get; set; }

        public string AttributeJson { get; set; }

        public decimal? ComparePrice { get; set; } // = 100

        public decimal Price { get; set; } //= 80

        public int Quantity { get; set; }

        public decimal Tax { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Discount { get; set; } //=10

        public decimal FinalPrice { get; set; } //=70

        public string TaxName { get; set; }

        public bool IsDownloadable { get; set; }

        #region Virtual Properties
        public virtual Product Product { get; set; }
        #endregion
    }
}