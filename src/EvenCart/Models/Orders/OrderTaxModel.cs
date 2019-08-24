using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    /// <summary>
    /// Represents a unique tax for a single order
    /// </summary>
    public class OrderTaxModel : FoundationModel
    {
        public string TaxName { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal Amount { get; set; }
    }
}