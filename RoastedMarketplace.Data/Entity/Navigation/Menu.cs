using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Navigation
{
    public class Menu : FoundationEntity
    {
        public string Name { get; set; }

        #region Virtual Properties
        public virtual IList<MenuItem> MenuItems { get; set; }
        #endregion
    }
}