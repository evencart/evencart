using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class UserRegistrationReportModel : FoundationModel
    {
        /// <summary>
        /// The group name of the response object
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Total number of users registered
        /// </summary>
        public int TotalUsers { get; set; }
    }
}