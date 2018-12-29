using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Addresses
{
    public class AddressInfoModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Landmark { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public string ZipPostalCode { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public AddressType AddressType { get; set; } = AddressType.Home;
    }
}