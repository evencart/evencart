using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Shop
{
    public class ProductVariantAttribute : FoundationEntity
    {
        public int ProductVariantId { get; set; }

        public int ProductAttributeId { get; set; }

        public int ProductAttributeValueId { get; set; }

        #region Virtual Properties
        public virtual ProductVariant ProductVariant { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }

        public virtual ProductAttributeValue ProductAttributeValue { get; set; }
        #endregion
    }
}