using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Countries
{
    public class StateOrProvinceModel : FoundationEntityModel
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public bool Published { get; set; }

        public bool ShippingEnabled { get; set; }

        public int DisplayOrder { get; set; }

    }
}