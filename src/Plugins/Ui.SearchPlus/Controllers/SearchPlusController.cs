using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Ui.SearchPlus.Factories;
using Ui.SearchPlus.Models;
using Ui.SearchPlus.Services;

namespace Ui.SearchPlus.Controllers
{
    [PluginType(PluginType = typeof(UiSearchPlusPlugin))]
    public class SearchPlusController : FoundationPluginController
    {
        private readonly ISearchTermService _searchTermService;
        private readonly IProductService _productService;
        private readonly SearchPlusSettings _searchPlusSettings;
        private readonly ISearchTermModelFactory _searchTermModelFactory;
        public SearchPlusController(ISearchTermService searchTermService, IProductService productService, SearchPlusSettings searchPlusSettings, ISearchTermModelFactory searchTermModelFactory)
        {
            _searchTermService = searchTermService;
            _productService = productService;
            _searchPlusSettings = searchPlusSettings;
            _searchTermModelFactory = searchTermModelFactory;
        }

        [DualGet("~/search-plus/autocomplete", Name = UiSearchPlusRouteNames.UiSearchAutoComplete, OnlyApi = true)]
        public IActionResult GetAutoCompleteResults([FromQuery] SearchModel searchModel)
        {
            if (searchModel == null || searchModel.Term.IsNullEmptyOrWhiteSpace())
                return R.Success.With("products", null).With("terms", null).With("totalProducts", 0).Result;

            var term = searchModel.Term;
            var searchTerms = _searchTermService.Get(out _, x => x.Term.StartsWith(term), x => x.Score, RowOrder.Descending, count: _searchPlusSettings.NumberOfAutoCompleteResults).ToList();
            //matching products
            var products = _productService.Get(out var totalProducts, x => x.Name.Contains(term),
                x => x.PopularityIndex,
                RowOrder.Descending, 1, _searchPlusSettings.NumberOfAutoCompleteResults).ToList();

            var productsModel = products.Select(_searchTermModelFactory.Create).ToList();
            var termsModel = searchTerms.Select(_searchTermModelFactory.Create).ToList();
            return R.Success.With("products", productsModel).With("terms", termsModel)
                .With("totalProducts", totalProducts).Result;
        }
    }
}