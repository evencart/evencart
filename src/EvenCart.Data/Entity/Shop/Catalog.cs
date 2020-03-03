using System.Collections.Generic;
using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class Catalog : FoundationEntity, IStoreEntity
    {
        public string Name { get; set; }

        public bool IsCountrySpecific { get; set; }

        #region Virtual Properties
        public virtual IList<Store> Stores { get; set; }

        public virtual IList<int> StoreIds { get; set; }
        #endregion
    }
}