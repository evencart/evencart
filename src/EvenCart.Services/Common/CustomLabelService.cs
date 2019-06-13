using System;
using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Common;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Common
{
    public class CustomLabelService : FoundationEntityService<CustomLabel>, ICustomLabelService
    {
        public IEnumerable<CustomLabel> Get(IList<CustomLabelType> enumTypes, out int totalResults, int page = 1, int count = Int32.MaxValue)
        {
            return Repository.Where(x => enumTypes.Contains(x.LabelType)).OrderBy(x => x.DisplayOrder)
                .SelectWithTotalMatches(out totalResults, page, count);
        }

        public IEnumerable<CustomLabel> GetCustomLabels(CustomLabelType enumType, out int totalResults, string searchText = null, int page = 1,
            int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Text.StartsWith(searchText));
            return query.Where(x => x.LabelType == enumType).OrderBy(x => x.DisplayOrder)
                .SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}