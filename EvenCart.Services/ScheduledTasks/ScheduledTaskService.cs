using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.ScheduledTasks;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.ScheduledTasks
{
    public class ScheduledTaskService : FoundationEntityService<ScheduledTask>, IScheduledTaskService
    {
        public IEnumerable<ScheduledTask> GeScheduledTasks(out int totalMatches, string searchText = null, bool? enableStatus = null, int page = 1,
            int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.StartsWith(searchText));
            if (enableStatus.HasValue)
                query = query.Where(x => x.Enabled == enableStatus);

            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalMatches, page, count);
        }
    }
}
