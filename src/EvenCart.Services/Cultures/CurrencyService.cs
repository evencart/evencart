using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Cultures
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