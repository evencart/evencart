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
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Pages
{
    public class ContentPage : FoundationEntity, ISeoEntity, IAllowsParent<ContentPage>, IStoreEntity, IMultilingualEntity
    {
        [MultilingualField]
        public string Name { get; set; }

        public int UserId { get; set; }

        [MultilingualField]
        public string Content { get; set; }

        public bool Published { get; set; }

        public bool Private { get; set; }

        public string Password { get; set; }

        public string SystemName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Template { get; set; }

        public int ParentId { get; set; }

        public string TranslationGuid { get; set; }

        #region Virtual Properties
        
        public virtual IList<TranslationData> Translations { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }

        public virtual User User { get; set; }

        public virtual ContentPage Parent { get; set; }
        
        public virtual IList<ContentPage> Children { get; set; }

        public virtual IList<int> StoreIds { get; set; }

        public virtual IList<Store> Stores { get; set; }
        #endregion
    }
}