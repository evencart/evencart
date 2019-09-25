using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Common
{
    public class CustomLabelService : FoundationEntityService<CustomLabel>, ICustomLabelService
    {
        public IEnumerable<CustomLabel> Get(IList<string> enumTypes, out int totalResults, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Where(x => enumTypes.Contains(x.Type)).OrderBy(x => x.DisplayOrder)
                .SelectWithTotalMatches(out totalResults, page, count);
        }

        public IEnumerable<CustomLabel> GetCustomLabels(string enumType, out int totalResults, string searchText = null, int page = 1,
            int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Text.StartsWith(searchText));
            return query.Where(x => x.Type == enumType).OrderBy(x => x.DisplayOrder)
                .SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}