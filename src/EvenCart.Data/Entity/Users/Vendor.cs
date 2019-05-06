using System.Collections.Generic;
using EvenCart.Core.Data;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Data.Entity.Users
{
    public class Vendor : FoundationEntity
    {
        public string Name { get; set; }

        public string GstNumber { get; set; }

        public string Tin { get; set; }

        public string Pan { get; set; }

        public string Address { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public int CountryId { get; set; }

        public string ZipPostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        #region Virtual Properties
        public virtual IList<User> Users { get; set; }

        public virtual IList<Product> Products { get; set; }

        public virtual SeoMeta SeoMeta { get; set; }
        #endregion
    }
}