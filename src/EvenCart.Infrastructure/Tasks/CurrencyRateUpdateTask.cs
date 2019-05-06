using System.Linq;
using System.Threading.Tasks;
using EvenCart.Core.Services;
using EvenCart.Core.Tasks;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Cultures;
using EvenCart.Services.Helpers;

namespace EvenCart.Infrastructure.Tasks
{
    public class CurrencyRateUpdateTask : ITask
    {
        private readonly LocalizationSettings _localizationSettings;
        private readonly ICurrencyService _currencyService;
        public CurrencyRateUpdateTask(LocalizationSettings localizationSettings, ICurrencyService currencyService)
        {
            _localizationSettings = localizationSettings;
            _currencyService = currencyService;
        }

        public void Dispose()
        {
            //nothing
        }

        public async Task Run()
        {
            //we only run the updater task if there are more than one currencies
            var currencies = _currencyService.Get(x => x.Published).ToList();
            if (currencies.Count < 2)
                return; //no need to run if we have less than two currencies...what'll we update?
            var providerName = _localizationSettings.DefaultCurrencyRateProvider;
            var provider = CurrencyProviderHelper.GetProvider(providerName);
            if (provider == null)
                return; // we don't have any provider...though there should always be the default one...but check anyways
            var baseCurrency = currencies.First(x => x.Id == _localizationSettings.BaseCurrencyId);
            var currencyRates = await provider.GetLatestRates(baseCurrency.IsoCode);
            Transaction.Initiate(transaction =>
            {
                foreach (var currency in currencies)
                {
                    if (currency.Equals(baseCurrency))
                        continue;
                    var currencyRate = currencyRates.First(x => x.IsoCode == currency.IsoCode);
                    currency.ExchangeRate = currencyRate.Rate;
                    _currencyService.Update(currency, transaction);
                }
            });
            
        }

        public string SystemName => "EvenCart.Infrastructure.Tasks.CurrencyRateUpdateTask";

        public string Name => "Currency Rate Updater";

        public int DefaultCycleDurationInSeconds => 60 * 60 * 24; //once a day
    }
}