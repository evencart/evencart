using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.ScheduledTasks
{
    public class ScheduledTaskSearchModel : AdminSearchModel
    {
        public bool? EnableStatus { get; set; }
    }
}