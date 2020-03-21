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
using EvenCart.Areas.Administration.Models.Cultures;
using EvenCart.Core.Data;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Services.Cultures;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class CulturesController : FoundationAdminController
    {
        private readonly ICurrencyService _currencyService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly ICurrencyModelFactory _currencyModelFactory;
        public CulturesController(ICurrencyService currencyService, IModelMapper modelMapper, IDataSerializer dataSerializer, ICurrencyModelFactory currencyModelFactory)
        {
            _currencyService = currencyService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _currencyModelFactory = currencyModelFactory;
        }

        [DualGet("currencieslist", Name = AdminRouteNames.CurrenciesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageCurrencies)]
        public IActionResult CurrenciesList(CurrencySearchModel searchModel)
        {
            var currencies = _currencyService.SearchCurrencies(out var totalResults, searchModel.SearchPhrase, searchModel.Current,
                searchModel.RowCount);

            var currenciesModel = currencies.Select(_currencyModelFactory.Create).ToList();
            return R.Success.With("currencies", currenciesModel)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("{currencyId}", Name = AdminRouteNames.GetCurrency)]
        [CapabilityRequired(CapabilitySystemNames.ManageCurrencies)]
        public IActionResult CurrencyEditor(int currencyId)
        {
            var currency = currencyId > 0 ? _currencyService.Get(currencyId) : new Currency();
            if (currency == null)
                return NotFound();
            var model = _currencyModelFactory.Create(currency);
            return R.Success.With("currency", model).WithAllFlags().WithCultures().WithRoundingTypes().Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveCurrency)]
        [CapabilityRequired(CapabilitySystemNames.ManageCurrencies)]
        [ValidateModelState(ModelType = typeof(CurrencyModel))]
        public IActionResult SaveCurrency(CurrencyModel currencyModel)
        {
            var currency = currencyModel.Id > 0 ? _currencyService.Get(currencyModel.Id) : new Currency();
            if (currency == null)
                return NotFound();

            _modelMapper.Map(currencyModel, currency);
            _currencyService.InsertOrUpdate(currency);
            return R.Success.With("id", currency.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteCurrency)]
        [CapabilityRequired(CapabilitySystemNames.ManageCurrencies)]
        public IActionResult DeleteCurrency(int currencyId)
        {
            var currency = currencyId > 0 ? _currencyService.Get(currencyId) : new Currency();
            if (currency == null)
                return NotFound();

            _currencyService.Delete(currency);
            return R.Success.Result;
        }
    }
}