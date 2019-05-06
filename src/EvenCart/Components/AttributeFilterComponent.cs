using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Services.Search;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Models.Components;
using EvenCart.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Components
{
    [ViewComponent(Name = "AttributeFilter")]
    public class AttributeFilterComponent : FoundationComponent
    {
        private readonly ISearchQueryParserService _searchQueryParserService;
        public AttributeFilterComponent(ISearchQueryParserService searchQueryParserService)
        {
            _searchQueryParserService = searchQueryParserService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            var searchModel = dataAsDict["model"] as ProductSearchModel;
            if (searchModel == null)
                return null;
            var selectedFilters = _searchQueryParserService.ParseToDictionary(searchModel.Filters);
            var availableFilterModels = searchModel.AvailableFilters.Select(x =>
            {
                var model = new AttributeFilterModel()
                {
                    FilterTitle = x.Key,
                    FilterValues = x.Value.Select(y =>
                        {
                            return new SelectListItem()
                            {
                                Text = y,
                                Value = y,
                                Selected = selectedFilters != null && selectedFilters.ContainsKey(x.Key) &&
                                           selectedFilters[x.Key].Contains(y, StringComparer.InvariantCultureIgnoreCase)
                            };
                        })
                        .ToList()
                };
                return model;
            }).ToList();
            return R.Success.With("filters", availableFilterModels).ComponentResult;
        }
    }
}