using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Factories;
using RoastedMarketplace.Areas.Administration.Models.Cultures;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Cultures;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
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
            return R.Success.With("currencies", () => currenciesModel,
                    () => _dataSerializer.Serialize(currenciesModel))
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