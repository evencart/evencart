using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class ProductWishReportModel : FoundationModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int TotalWishes { get; set; }

        public bool Published { get; set; }
    }
}