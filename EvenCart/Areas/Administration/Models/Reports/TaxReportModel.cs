using System;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class TaxReportModel : FoundationModel
    {
        public decimal TotalTax { get; set; }

        public int TotalOrders { get; set; }

        public string TotalTaxFormatted => TotalTax.ToCurrency();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}