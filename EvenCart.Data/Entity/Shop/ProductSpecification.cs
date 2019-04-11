using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductSpecification : FoundationEntity
    {
        public int ProductId { get; set; }

        public int AvailableAttributeId { get; set; }

        public string Label { get; set; }

        public int DisplayOrder { get; set; }

        public int ProductSpecificationGroupId { get; set; }

        public bool IsVisible { get; set; }

        public bool IsFilterable { get; set; }

        #region Virtual Properties

        public virtual IList<ProductSpecificationValue> ProductSpecificationValues { get; set; }

        public virtual Product Product { get; set; }

        public virtual AvailableAttribute AvailableAttribute { get; set; }

        public virtual ProductSpecificationGroup ProductSpecificationGroup { get; set; }

        #endregion
    }
}