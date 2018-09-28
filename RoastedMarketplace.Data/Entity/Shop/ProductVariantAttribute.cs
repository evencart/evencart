using System.Security.Cryptography;
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

        #region Overrides

        public override bool Equals(object obj)
        {
            var objAsVariantAttribute = (ProductVariantAttribute) obj;
            return objAsVariantAttribute.ProductAttributeId == this.ProductAttributeId &&
                   objAsVariantAttribute.ProductAttributeValueId == this.ProductAttributeValueId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // Maybe nullity checks, if these are objects not primitives!
                hash = hash * 23 + ProductAttributeId.GetHashCode();
                hash = hash * 23 + ProductAttributeValueId.GetHashCode();
                return hash;
            }
        }

        #endregion
    }
}