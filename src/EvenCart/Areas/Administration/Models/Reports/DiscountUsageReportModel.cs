using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class DiscountUsageReportModel : FoundationModel
    {
        public string DiscountCoupon { get; set; }

        public int DiscountId { get; set; }

        public int TotalOrders { get; set; }
    }
}