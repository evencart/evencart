using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    [FormatAsCurrencies(nameof(TotalAmount), CurrencyCodeProperty = nameof(CurrencyCode))]
    public class UserOrderReportModel : FoundationModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public int Id { get; set; }

        public decimal TotalAmount { get; set; }

        public string CurrencyCode { get; set; }

        public int TotalOrders { get; set; }

        public int TotalProducts { get; set; }

        public string TotalAmountFormatted => TotalAmount.ToCurrency();
    }
}