﻿#region License
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
using EvenCart.Areas.Administration.Models.Countries;
using Genesis;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Addresses;
using Genesis.Modules.Data;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;
using EvenCart.Genesis.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class CountriesController : GenesisAdminController
    {
        private readonly ICountryService _countryService;
        private readonly IStateOrProvinceService _stateOrProvinceService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public CountriesController(ICountryService countryService, IModelMapper modelMapper, IDataSerializer dataSerializer, IStateOrProvinceService stateOrProvinceService)
        {
            _countryService = countryService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _stateOrProvinceService = stateOrProvinceService;
        }

        #region Country
        [DualGet("", Name = AdminRouteNames.CountriesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        [ValidateModelState(ModelType = typeof(AdminSearchModel))]
        public IActionResult CountriesList(CountrySearchModel searchModel)
        {
            
            var countries = _countryService.GetCountries(out int totalResults, searchModel.SearchPhrase, searchModel.Current,
                searchModel.RowCount);

            var models = countries.Select(x => _modelMapper.Map<CountryModel>(x)).ToList();
            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("countries", models)
                .WithParams(searchModel)
                .Result;
        }

        [DualGet("{countryId}", Name = AdminRouteNames.GetCountry)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        public IActionResult CountryEditor(int countryId)
        {
            var country = countryId > 0 ? _countryService.Get(countryId) : null;
            if (country == null)
                return NotFound();
            var countryModel = _modelMapper.Map<CountryModel>(country);
            return R.Success.With("country", countryModel).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveCountry, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        public IActionResult SaveCountry(CountryModel countryModel)
        {
            var country = countryModel.Id > 0 ? _countryService.Get(countryModel.Id) : null;
            if (country == null)
                return NotFound();

            country.DisplayOrder = countryModel.DisplayOrder;
            country.Published = countryModel.Published;
            country.ShippingEnabled = countryModel.ShippingEnabled;
            _countryService.InsertOrUpdate(country);
            return R.Success.Result;
        }

        #endregion

        #region States
        [DualGet("{countryId}/states", Name = AdminRouteNames.StatesList)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        public IActionResult StatesList(StateSearchModel searchModel)
        {
            var countryId = searchModel.CountryId;
            Country country = null;
            if (countryId <= 0 || (country = _countryService.FirstOrDefault(x => x.Id == countryId)) == null)
                return NotFound();
            var stateOrProvinces = _stateOrProvinceService.GetStateOrProvinces(out int totalResults, countryId,
                searchModel.SearchPhrase, searchModel.Current, searchModel.RowCount);

            var models = stateOrProvinces.Select(x => _modelMapper.Map<StateOrProvinceModel>(x)).ToList();

            return R.Success.With("states", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("countryId", countryId)
                .With("countryName", country.Name)
                .Result;
        }

        [DualGet("{countryId}/states/{stateId}", Name = AdminRouteNames.GetState)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        public IActionResult StateEditor(int countryId, int stateId)
        {
            if (countryId <= 0 || _countryService.Count(x => x.Id == countryId) == 0)
                return NotFound();

            var stateOrProvince = stateId > 0 ? _stateOrProvinceService.Get(stateId) : new StateOrProvince();
            if (stateOrProvince == null)
                return NotFound();
            var stateOrProvinceModel = _modelMapper.Map<StateOrProvinceModel>(stateOrProvince);
            stateOrProvinceModel.CountryId = countryId;
            return R.Success.With("state", stateOrProvinceModel).With("countryId", countryId).WithAvailableCountries().Result;
        }

        [DualPost("{countryId}/states", Name = AdminRouteNames.SaveState, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        [ValidateModelState(ModelType = typeof(StateOrProvinceModel))]
        public IActionResult SaveStateOrProvince(StateOrProvinceModel stateOrProvinceModel)
        {
            if (_countryService.Count(x => x.Id == stateOrProvinceModel.CountryId) == 0)
                return NotFound();

            var stateOrProvince = stateOrProvinceModel.Id > 0 ? _stateOrProvinceService.Get(stateOrProvinceModel.Id) : new StateOrProvince();
            if (stateOrProvince == null)
                return NotFound();

            _modelMapper.Map(stateOrProvinceModel, stateOrProvince);
            _stateOrProvinceService.InsertOrUpdate(stateOrProvince);
            return R.Success.Result;
        }

        [DualPost("states/delete", Name = AdminRouteNames.DeleteState, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageCountries)]
        public IActionResult DeleteStateOrProvince(int stateId)
        {
            var stateOrProvince = stateId > 0 ? _stateOrProvinceService.Get(stateId) : null;
            if (stateOrProvince == null)
                return NotFound();

            _stateOrProvinceService.Delete(stateOrProvince);
            return R.Success.Result;
        }
        #endregion
    }
}