using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Factories;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Services.Cultures;

namespace RoastedMarketplace.Components
{
    [ViewComponent(Name = "CurrencySelector")]
    public class CurrencySelectorComponent : FoundationComponent
    {
        private readonly ICurrencyService _currencyService;
        private readonly ICurrencyModelFactory _currencyModelFactory;
        private readonly LocalizationSettings _localizationSettings;
        public CurrencySelectorComponent(ICurrencyService currencyService, ICurrencyModelFactory currencyModelFactory, LocalizationSettings localizationSettings)
        {
            _currencyService = currencyService;
            _currencyModelFactory = currencyModelFactory;
            _localizationSettings = localizationSettings;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            if (!_localizationSettings.AllowUserToSelectCurrency)
                return R.Success.ComponentResult;

            var currencies = _currencyService.Get(x => x.Published).ToList();
            if (currencies.Count < 2)
                return R.Success.ComponentResult; // no need to display the box at all if there are none or one currency

            var models = currencies.Select(_currencyModelFactory.Create).ToList();
            var activeCurrency = _currencyModelFactory.Create(ApplicationEngine.CurrentCurrency);
            return R.Success.With("currencies", models).With("activeCurrency", activeCurrency).ComponentResult;
        }
    }
}