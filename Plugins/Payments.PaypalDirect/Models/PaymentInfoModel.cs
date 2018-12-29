using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace Payments.PaypalDirect.Models
{
    public class PaymentInfoModel : FoundationModel
    {
        public PaymentInfoModel()
        {
            AvailableMonths = new List<SelectListItem>();
            AvailableYears = new List<SelectListItem>();
        }
        public IList<SelectListItem> AvailableMonths { get; set; }

        public IList<SelectListItem> AvailableYears { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}