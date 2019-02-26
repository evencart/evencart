using System;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Models.Orders
{
    public class OrderSearchModel : FoundationModel
    {
        public DateTime? FromDate { get; set; } = DateTime.UtcNow.AddMonths(-6); //last 6 months by default

        public DateTime? ToDate { get; set; } = DateTime.UtcNow;

        public string OrderStatus { get; set; } = "all";

        public int Current { get; set; } = 1;

        public int RowCount { get; set; } = 15;
    }
}