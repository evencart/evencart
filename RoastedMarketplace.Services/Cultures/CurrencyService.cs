using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Data.Extensions;

namespace RoastedMarketplace.Services.Cultures
{
    public class CurrencyService : FoundationEntityService<Currency>, ICurrencyService
    {
        public IEnumerable<Currency> SearchCurrencies(out int totalResults, string searchText = null, int page = 1, int count = 15)
        {
            var query = Repository;
            if (!searchText.IsNullEmptyOrWhiteSpace())
            {
                query = query.Where(x => x.Name.StartsWith(searchText) || x.IsoCode.StartsWith(searchText));
            }

            query = query.OrderBy(x => x.Name);
            return query.SelectWithTotalMatches(out totalResults, page, count);
        }

       
    }
}