using System.Collections.Generic;
using System.Threading.Tasks;
using EvenCart.Data.Entity.Cultures;

namespace EvenCart.Services.Cultures
{
    public interface ICurrencyRateProvider
    {
        Task<IList<CurrencyRate>> GetLatestRates(string baseCurrencyCode);

        string ProviderName { get; }
    }
}