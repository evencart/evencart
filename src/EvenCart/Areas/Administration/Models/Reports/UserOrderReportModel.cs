using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class UserOrderReportModel : FoundationModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public int Id { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalOrders { get; set; }

        public int TotalProducts { get; set; }

        public string TotalAmountFormatted => TotalAmount.ToCurrency();
    }
}