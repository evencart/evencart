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