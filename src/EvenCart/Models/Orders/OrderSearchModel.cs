using System;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Models.Orders
{
    public class OrderSearchModel : FoundationModel
    {
        /// <summary>
        /// The start date to search the orders from. Defaults to 6 months old
        /// </summary>
        public DateTime? FromDate { get; set; } = DateTime.UtcNow.AddMonths(-6); //last 6 months by default
        /// <summary>
        /// The end date to search the orders to. Defaults to today.
        /// </summary>
        public DateTime? ToDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The status of order. Can be one of all, open, closed, returned or cancelled
        /// </summary>
        public string OrderStatus { get; set; } = "all";

        /// <summary>
        /// The page being requested in a paginated request. Default is 1.
        /// </summary>
        public int Current { get; set; } = 1;

        /// <summary>
        /// The total number of result rows to be returned
        /// </summary>
        public int RowCount { get; set; } = 15;
    }
}