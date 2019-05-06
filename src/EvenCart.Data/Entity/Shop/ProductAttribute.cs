using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductAttribute : FoundationEntity
    {
        public int ProductId { get; set; }

        public int AvailableAttributeId { get; set; }

        public InputFieldType InputFieldType { get; set; }

        public string Label { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; }

        #region Virtual Properties

        public virtual IList<ProductAttributeValue> ProductAttributeValues { get; set; }

        public virtual Product Product { get; set; }

        public virtual AvailableAttribute AvailableAttribute { get; set; }
        
        #endregion
    }
}