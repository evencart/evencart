using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.ScheduledTasks
{
    public class ScheduledTaskSearchModel : AdminSearchModel
    {
        public bool? EnableStatus { get; set; }
    }
}