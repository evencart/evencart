using System.Collections.Generic;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Cultures;

namespace RoastedMarketplace.Services.Cultures
{
    public interface ICurrencyService : IFoundationEntityService<Currency>
    {
        IEnumerable<Currency> SearchCurrencies(out int totalResults, string searchText = null, int page = 1,
            int count = 15);
    }
}