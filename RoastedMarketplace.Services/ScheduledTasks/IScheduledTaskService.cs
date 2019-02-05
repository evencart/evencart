using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.ScheduledTasks;

namespace RoastedMarketplace.Services.ScheduledTasks
{
    public interface IScheduledTaskService : IFoundationEntityService<ScheduledTask>
    {
        IEnumerable<ScheduledTask> GeScheduledTasks(out int totalMatches, string searchText = null, bool? enableStatus = null, int page = 1,
            int count = int.MaxValue);
    }
}