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
using EvenCart.Models.Countries;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Addresses;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows to retrieve list of available countries and states from store database
    /// </summary>
    [Route("countries")]
    public class CountriesController : GenesisController
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