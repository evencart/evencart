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

using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Addresses
{
    public class StateOrProvinceService : FoundationEntityService<StateOrProvince>, IStateOrProvinceService
    {
        public IEnumerable<StateOrProvince> GetStateOrProvinces(out int totalMatches, int countryId, string search = null, int page = 1, int count = 15)
        {
            var query = Repository.Where(x => x.CountryId == countryId);
            if (!search.IsNullEmptyOrWhiteSpace())
                query = query.Where(x => x.Name.StartsWith(search));
            return query.OrderBy(x => x.Name)
                .SelectWithTotalMatches(out totalMatches, page, count);

        }
    }
}