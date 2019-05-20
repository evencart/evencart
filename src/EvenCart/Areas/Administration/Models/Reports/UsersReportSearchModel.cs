using System;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class UsersReportSearchModel : AdminSearchModel
    {
        /// <summary>
        /// The start date for search.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date for search
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The unit by which results should be grouped
        /// </summary>
        public GroupUnit GroupBy { get; set; }
    }
}