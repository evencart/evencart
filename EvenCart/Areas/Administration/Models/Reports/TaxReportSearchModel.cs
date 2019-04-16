using System;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class TaxReportSearchModel : AdminSearchModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}