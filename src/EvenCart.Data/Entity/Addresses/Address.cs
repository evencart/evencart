using EvenCart.Core.Data;

namespace EvenCart.Data.Entity.Addresses
{
    /// <summary>
    /// Addresses associated with various entities such as User, Page etc.
    /// </summary>
    public class Address : FoundationEntity
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Landmark { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public string ZipPostalCode { get; set; }

        public int CountryId { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public AddressType AddressType { get; set; }

        #region Virtual Properties
        public virtual Country Country { get; set; }

        public virtual StateOrProvince StateOrProvince { get; set; }
        #endregion
    }
}