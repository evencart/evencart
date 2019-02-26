using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Countries
{
    public class CountryModel : FoundationEntityModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public bool Published { get; set; }

        public bool ShippingEnabled { get; set; }

        public int DisplayOrder { get; set; }
    }
}