using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Countries
{
    public class StateSearchModel : AdminSearchModel
    {
        public int CountryId { get; set; }
    }
}