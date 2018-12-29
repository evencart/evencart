using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Addresses
{
    public class Country : FoundationEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        #region Virtual Properties
        public virtual IList<StateOrProvince> StateOrProvinces { get; set; }
        #endregion
    }
}