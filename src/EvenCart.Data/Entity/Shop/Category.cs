using System;
using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Data.Entity.Shop
{
    public class Category : FoundationEntity, ISeoEntity, IAllowsParent<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public int? TaxId { get; set; }

        public int ParentId { get; set; }

        public int MediaId { get; set; }

        #region Virtual Properties

        public virtual IList<Category> Children { get; set; }

        public virtual Category Parent { get; set; }

        public virtual Media Media { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }

        [Obsolete("The property is obsolete. Use ParentId instead.")]
        public virtual int ParentCategoryId { get; set; }
        #endregion
    }
}