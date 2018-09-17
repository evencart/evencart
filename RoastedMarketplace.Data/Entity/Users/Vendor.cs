using System.Collections.Generic;
using RoastedMarketplace.Core.Data;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Data.Entity.Users
{
    public class Vendor : FoundationEntity
    {
        public string VendorName { get; set; }

        public string GstNumber { get; set; }

        public string Tin { get; set; }

        public string Pan { get; set; }

        public string Address { get; set; }

        #region Virtual Properties
        public virtual IList<User> Users { get; set; }

        public virtual IList<Product> Products { get; set; }
        #endregion
    }
}