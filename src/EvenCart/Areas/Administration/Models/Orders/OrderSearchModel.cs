using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class OrderSearchModel : AdminSearchModel
    {
        public int[] OrderIds { get; set; }

        public int[] VendorIds { get; set; }

        public int? UserId { get; set; }

        public string Email { get; set; }

        public int[] ProductIds { get; set; }

        public IList<OrderStatus> OrderStatus { get; set; }

        public IList<PaymentStatus> PaymentStatus { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}