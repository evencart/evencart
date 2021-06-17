#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using Genesis.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductVariantAttribute : GenesisEntity
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
            if (obj == null || obj.GetType() != typeof(ProductVariantAttribute))
                return false;
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