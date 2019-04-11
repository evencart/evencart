using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.MediaEntities;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Data.Entity.Shop
{
    public class Category : FoundationEntity, ISeoEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public int? TaxId { get; set; }

        public int ParentCategoryId { get; set; }

        public int MediaId { get; set; }

        #region Virtual Properties

        public virtual IList<Category> ChildCategories { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual Media Media { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }
        #endregion
    }
}