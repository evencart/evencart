using System;
using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Data.Entity.Pages
{
    public class ContentPage : FoundationEntity, ISeoEntity, IAllowsParent<ContentPage>, IStoreEntity
    {
        public string Name { get; set; }

        public int UserId { get; set; }

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

        #region Virtual Properties
        public virtual SeoMeta SeoMeta { get; set; }

        public virtual User User { get; set; }

        public virtual ContentPage Parent { get; set; }
        
        public virtual IList<ContentPage> Children { get; set; }

        public virtual IList<int> StoreIds { get; set; }

        public virtual IList<Store> Stores { get; set; }
        #endregion
    }
}