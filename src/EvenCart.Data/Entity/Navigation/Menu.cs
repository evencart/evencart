using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Entity.Navigation
{
    public class Menu : FoundationEntity, IStoreEntity
    {
        public string Name { get; set; }

        #region Virtual Properties
        public virtual IList<MenuItem> MenuItems { get; set; }

        public virtual IList<int> StoreIds { get; set; }

        public virtual IList<Store> Stores { get; set; }
        #endregion
    }
}