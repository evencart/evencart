using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;
using Ui.SearchPlus.Data;
using Ui.SearchPlus.Models.Administration;
using Ui.SearchPlus.Services;

namespace Ui.SearchPlus.Controllers.Administration
{
    public class SearchPlusController : FoundationAdminController
    {
        private readonly ISearchTermService _searchTermService;
        private readonly IModelMapper _modelMapper;
        private readonly SearchPlusSettings _searchPlusSettings;
        public SearchPlusController(ISearchTermService searchTermService, IModelMapper modelMapper, SearchPlusSettings searchPlusSettings)
        {
            _searchTermService = searchTermService;
            _modelMapper = modelMapper;
            _searchPlusSettings = searchPlusSettings;
        }

        [DualGet("configure", Name = UiSearchPlusRouteNames.UiSearchPlusConfigure)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        public IActionResult SearchTermsConfigure()
        {
            var model = new SearchPlusSettingsModel()
            {
                NumberOfAutoCompleteResults = _searchPlusSettings.NumberOfAutoCompleteResults,
                ShowTermCategory = _searchPlusSettings.ShowTermCategory
            };
            return R.Success.With("settings", model).Result;
        }

        [DualPost("configure", Name = UiSearchPlusRouteNames.UiSearchPlusConfigure, OnlyApi = true)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        public IActionResult SearchTermsConfigure(SearchPlusSettingsModel settingsModel)
        {
            _searchPlusSettings.NumberOfAutoCompleteResults = settingsModel.NumberOfAutoCompleteResults;
            _searchPlusSettings.ShowTermCategory = settingsModel.ShowTermCategory;
            return R.Success.Result;
        }

        [DualGet("terms", Name = UiSearchPlusRouteNames.UiSearchPlusTermsList)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        public IActionResult SearchTermsList([FromQuery] SearchTermSearchModel searchModel)
        {
            var searchTerms = _searchTermService.Get(out var totalResults,
                x => x.Term.StartsWith(searchModel.SearchPhrase), x => x.Score, RowOrder.Descending,
                searchModel.Current, searchModel.RowCount);

            var models = searchTerms.Select(x => _modelMapper.Map<SearchTermModel>(x)).ToList();
            return R.Success.With("searchTerms", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualPost("", Name = UiSearchPlusRouteNames.UiSaveSearchTerm)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        [ValidateModelState(ModelType = typeof(SearchTermModel))]
        public IActionResult SaveSearchTerm(SearchTermModel searchTermModel)
        {
            var searchTerm = searchTermModel.Id > 0 ? _searchTermService.Get(searchTermModel.Id) : new SearchTerm();
            if (searchTerm == null)
                return NotFound();
            
            _modelMapper.Map(searchTermModel, searchTerm);
            _searchTermService.InsertOrUpdate(searchTerm);
       
            return R.Success.With("id", searchTerm.Id).Result;
        }

        [DualGet("{searchTermId}", Name = UiSearchPlusRouteNames.UiSearchGetSearchTerm)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        public IActionResult SearchTermEditor(int searchTermId)
        {
            var searchTerm = searchTermId > 0 ? _searchTermService.Get(searchTermId) : new SearchTerm();
            var model = new SearchTermModel()
            {
                Id = searchTermId,
                Score = searchTerm.Score,
                Term = searchTerm.Term,
                TermCategory = searchTerm.TermCategory
            };
            return R.Success.With("searchTerm", model).Result;
        }

        [DualPost("{searchTermId}", Name = UiSearchPlusRouteNames.UiDeleteSearchTerm)]
        [CapabilityRequired(SearchPlusCapabilityNames.ManageSearchPlus)]
        public IActionResult DeleteSearchTerm(int searchTermId)
        {
            if (searchTermId <= 0 || _searchTermService.Count(x => x.Id == searchTermId) == 0)
                return NotFound();
            _searchTermService.Delete(x => x.Id == searchTermId);
            return R.Success.Result;
        }
    }
}