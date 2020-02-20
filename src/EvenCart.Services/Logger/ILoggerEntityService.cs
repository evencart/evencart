using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Logs;

namespace EvenCart.Services.Logger
{
    public interface ILoggerEntityService : IFoundationEntityService<Log>
    {
        IList<Log> GetLogs(out int totalResults, string search = null, int page = 1, int count = int.MaxValue);
    }
}