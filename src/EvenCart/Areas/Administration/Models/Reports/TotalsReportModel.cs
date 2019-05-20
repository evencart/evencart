using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    /// <summary>
    /// Contains information about various totals
    /// </summary>
    public class TotalsReportModel : FoundationModel
    {
        /// <summary>
        /// The total users in the store
        /// </summary>
        public int TotalUsers { get; set; }

        /// <summary>
        /// Total orders in the store
        /// </summary>
        public int TotalOrders { get; set; }

        /// <summary>
        /// Total pending orders in the store
        /// </summary>
        public int TotalPendingOrders { get; set; }

        /// <summary>
        /// Total paid amount in the store
        /// </summary>
        public decimal TotalPaidAmount { get; set; }

        /// <summary>
        /// Total paid orders in the store
        /// </summary>
        public int TotalPaidOrders { get; set; }

        /// <summary>
        /// Total cancelled orders in the store
        /// </summary>
        public int TotalCancelledOrders { get; set; }

        /// <summary>
        /// Total returned orders
        /// </summary>
        public int TotalReturnedOrders { get; set; }

        /// <summary>
        /// Total shipped orders
        /// </summary>
        public int TotalShippedOrders { get; set; }

        /// <summary>
        /// Total partially shipped orders
        /// </summary>
        public int TotalPartiallyShippedOrders { get; set; }

        /// <summary>
        /// Total complete orders
        /// </summary>
        public int TotalCompleteOrders { get; set; }

        /// <summary>
        /// Total delayed orders
        /// </summary>
        public int TotalDelayedOrders { get; set; }

        /// <summary>
        /// Total closed orders
        /// </summary>
        public int TotalClosedOrders { get; set; }

        /// <summary>
        /// Total closed orders
        /// </summary>
        public int TotalOnholdOrders { get; set; }

        /// <summary>
        /// Total new orders
        /// </summary>
        public int TotalNewOrders { get; set; }

        /// <summary>
        /// Total processing orders
        /// </summary>
        public int TotalProcessingOrders { get; set; }
    }
}