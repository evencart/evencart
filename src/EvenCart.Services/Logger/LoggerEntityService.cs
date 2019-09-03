#region Author Information
// LoggerEntityService.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Core.Data;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Logs;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Logger
{
    public class LoggerEntityService : FoundationEntityService<Log>, ILoggerEntityService
    {
        public IList<Log> GetLogs(out int totalResults, string search = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;

            if (!search.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.ShortMessage.Contains(search) || x.Details.Contains(search));

            query = query.OrderBy(x => x.CreatedOn, RowOrder.Descending);
            return query.SelectWithTotalMatches(out totalResults, page, count).ToList();
        }
    }
}