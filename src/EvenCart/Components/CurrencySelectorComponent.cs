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

using System.Linq;
using EvenCart.Areas.Administration.Factories.Cultures;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Localization;
using Genesis.Modules.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "CurrencySelector")]
    public class CurrencySelectorComponent : GenesisComponent
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
            var activeCurrency = _currencyModelFactory.Create(Engine.CurrentCurrency);
            return R.Success.With("currencies", models).With("activeCurrency", activeCurrency).ComponentResult;
        }
    }
}