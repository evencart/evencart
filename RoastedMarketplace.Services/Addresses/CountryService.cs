using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Addresses;

namespace RoastedMarketplace.Services.Addresses
{
    public class CountryService : FoundationEntityService<Country>, ICountryService
    {
        public IEnumerable<Country> GetCountries(out int totalResults, string search = null, int page = 1, int count = Int32.MaxValue)
        {
            var query = Repository;
            if (!search.IsNullEmptyOrWhitespace())
                query = query.Where(x => x.Name.Contains(search));
            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }
    }
}