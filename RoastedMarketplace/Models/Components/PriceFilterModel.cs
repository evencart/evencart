using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Components
{
    public class PriceFilterModel : FoundationModel
    {
        public decimal FromPrice { get; set; }

        public decimal ToPrice { get; set; }

        public decimal AvailableFromPrice { get; set; }

        public decimal AvailableToPrice { get; set; }

        public bool HasSelection => FromPrice != AvailableFromPrice || ToPrice != AvailableToPrice;
    }
}