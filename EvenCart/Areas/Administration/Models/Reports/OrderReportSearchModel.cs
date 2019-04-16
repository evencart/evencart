using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class OrderReportSearchModel : AdminSearchModel
    {
        public IList<OrderStatus> OrderStatus { get; set; }

        public IList<PaymentStatus> PaymentStatus { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public GroupUnit GroupBy { get; set; }
    }
}