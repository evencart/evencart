using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Countries;
using RoastedMarketplace.Services.Addresses;

namespace RoastedMarketplace.Controllers
{
    [Route("countries")]
    public class CountriesController : FoundationController
    {
        private readonly ICountryService _countryService;
        private readonly IStateOrProvinceService _stateOrProvinceService;
        private readonly IModelMapper _modelMapper;
        public CountriesController(ICountryService countryService, IStateOrProvinceService stateOrProvinceService, IModelMapper modelMapper)
        {
            _countryService = countryService;
            _stateOrProvinceService = stateOrProvinceService;
            _modelMapper = modelMapper;
        }

        [DualGet("{countryId}/states", Name = RouteNames.CountryStates, OnlyApi = true)]
        public IActionResult StatesList(int countryId)
        {
            Country country = null;
            if (countryId <= 0 || (country = _countryService.FirstOrDefault(x => x.Id == countryId)) == null)
                return NotFound();
            var stateOrProvinces = _stateOrProvinceService.GetStateOrProvinces(out _, countryId);

            var models = stateOrProvinces.Select(x => _modelMapper.Map<StateOrProvinceModel>(x)).ToList();

            return R.Success.With("states", models)
                .With("countryId", countryId)
                .With("countryName", country.Name)
                .Result;
        }
    }
}