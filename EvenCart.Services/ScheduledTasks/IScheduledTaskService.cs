using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.ScheduledTasks;

namespace EvenCart.Services.ScheduledTasks
{
    public interface IScheduledTaskService : IFoundationEntityService<ScheduledTask>
    {
        IEnumerable<ScheduledTask> GeScheduledTasks(out int totalMatches, string searchText = null, bool? enableStatus = null, int page = 1,
            int count = int.MaxValue);
    }
}