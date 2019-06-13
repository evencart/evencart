using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents a return request search object
    /// </summary>
    public class ReturnRequestSearchModel : AdminSearchModel
    {
        /// <summary>
        /// The list of <see cref="ReturnRequestStatus">status</see> values to restrict search results to
        /// </summary>
        public IList<ReturnRequestStatus> ReturnRequestStatus { get; set; }

        /// <summary>
        /// The start date to restrict the search results from
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// The end date to restrict the search results from
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}