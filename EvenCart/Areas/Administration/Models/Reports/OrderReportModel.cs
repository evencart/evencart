using System;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class OrderReportModel : FoundationModel
    {
        public decimal TotalAmount { get; set; }

        public int TotalOrders { get; set; }

        public int TotalProducts { get; set; }

        public string TotalAmountFormatted => TotalAmount.ToCurrency();

        public string GroupName { get; set; }
    }
}