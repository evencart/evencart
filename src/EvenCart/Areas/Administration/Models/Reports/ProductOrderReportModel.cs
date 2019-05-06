using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class ProductOrderReportModel : FoundationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalOrders { get; set; }

        public int TotalCustomers { get; set; }

        public bool Published { get; set; }
    }
}