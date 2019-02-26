using System.Collections.Generic;
using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Addresses
{
    public class Country : FoundationEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool Published { get; set; }

        public bool ShippingEnabled { get; set; }

        public int DisplayOrder { get; set; }

        #region Virtual Properties
        public virtual IList<StateOrProvince> StateOrProvinces { get; set; }
        #endregion
    }
}