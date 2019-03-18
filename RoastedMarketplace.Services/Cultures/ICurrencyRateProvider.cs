using System.Collections.Generic;
using System.Threading.Tasks;
using RoastedMarketplace.Data.Entity.Cultures;

namespace RoastedMarketplace.Services.Cultures
{
    public interface ICurrencyRateProvider
    {
        Task<IList<CurrencyRate>> GetLatestRates(string baseCurrencyCode);

        string ProviderName { get; }
    }
}