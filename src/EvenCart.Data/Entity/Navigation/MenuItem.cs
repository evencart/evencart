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

using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Pages;

namespace EvenCart.Data.Entity.Navigation
{
    public class MenuItem : FoundationEntity, IAllowsParent<MenuItem>
    {
        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public int? SeoMetaId { get; set; }

        public string Url { get; set; }

        public int DisplayOrder { get; set; }

        public string CssClass { get; set; }

        public bool IsGroup { get; set; }

        public bool OpenInNewWindow { get; set; }

        public string Description { get; set; }

        public string ExtraData { get; set; }

        #region Virtual Properties
        public virtual Menu Menu { get; set; }

        public virtual MenuItem Parent { get; set; }

        public virtual IList<MenuItem> Children { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }
        #endregion
    }
}