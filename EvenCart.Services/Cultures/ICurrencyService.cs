using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Cultures;

namespace EvenCart.Services.Cultures
{
    public interface ICurrencyService : IFoundationEntityService<Currency>
    {
        IEnumerable<Currency> SearchCurrencies(out int totalResults, string searchText = null, int page = 1,
            int count = 15);
    }
}