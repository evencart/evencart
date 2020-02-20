using System.Linq;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Services.Addresses;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Countries;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows to retrieve list of available countries and states from store database
    /// </summary>
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
        /// <summary>
        /// Gets the states for a country
        /// </summary>
        /// <param name="countryId">The id of country for which states to be retrieved</param>
        /// <response code="200">A list of <see cref="StateOrProvinceModel">states</see> of the country</response>
        [DualGet("{countryId}/states", Name = RouteNames.CountryStates, OnlyApi = true)]
        public IActionResult StatesList(int countryId)
        {
            Country country = null;
            if (countryId <= 0 || (country = _countryService.FirstOrDefault(x => x.Id == countryId)) == null)
                return NotFound();
            var stateOrProvinces = _stateOrProvinceService.GetStateOrProvinces(out _, countryId, count: int.MaxValue);

            var models = stateOrProvinces.Select(x => _modelMapper.Map<StateOrProvinceModel>(x)).ToList();

            return R.Success.With("states", models)
                .With("countryId", countryId)
                .With("countryName", country.Name)
                .Result;
        }
    }
}