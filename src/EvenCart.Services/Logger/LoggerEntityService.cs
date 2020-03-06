#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using DotEntity.Enumerations;
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