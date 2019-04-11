using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Components
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