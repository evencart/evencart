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