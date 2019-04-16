using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class StockReportSearchModel : AdminSearchModel
    {
        public string ProductSearch { get; set; }

        public bool? Published { get; set; }
    }
}