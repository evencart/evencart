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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Common;
using EvenCart.Areas.Administration.Models.Manufacturers;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class ManufacturersController : FoundationAdminController
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        public ManufacturersController(IManufacturerService manufacturerService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _manufacturerService = manufacturerService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }
        [DualGet("suggestions", Name = AdminRouteNames.GetManufacturerSuggestions, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ViewManufacturers)]
        public IActionResult Suggestions(string q = null)
        {
            var manufacturers = _manufacturerService.SearchManufacturers(out int totalResults, q, 1, 15);
            var model = new List<AutocompleteModel>();
            foreach (var c in manufacturers)
            {
                model.Add(new AutocompleteModel() {
                    Id = c.Id,
                    Text = c.Name
                });
            }
            return R.Success.With("suggestions", model.OrderBy(x => x.Text)).Result;
        }

        [DualGet("", Name = AdminRouteNames.ManufacturersList)]
        [CapabilityRequired(CapabilitySystemNames.ViewManufacturers)]
        [ValidateModelState(ModelType = typeof(ManufacturerSearchModel))]
        public IActionResult ManufacturersList(ManufacturerSearchModel searchModel)
        {
            var manufacturers = _manufacturerService.SearchManufacturers(out int totalResults, searchModel.SearchPhrase, searchModel.Current,
                searchModel.RowCount);

            var manufacturersModel = manufacturers.Select(x => _modelMapper.Map<ManufacturerModel>(x)).ToList();
            return R.Success.With("manufacturers", manufacturersModel)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("{manufacturerId}", Name = AdminRouteNames.GetManufacturer)]
        [CapabilityRequired(CapabilitySystemNames.EditManufacturer)]
        public IActionResult ManufacturerEditor(int manufacturerId)
        {
            var manufacturer = manufacturerId > 0 ? _manufacturerService.Get(manufacturerId) : new Manufacturer();
            if (manufacturer == null)
                return NotFound();
            var model = _modelMapper.Map<ManufacturerModel>(manufacturer);
            return R.Success.With("manufacturer", model).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveManufacturer)]
        [CapabilityRequired(CapabilitySystemNames.EditManufacturer)]
        [ValidateModelState(ModelType = typeof(ManufacturerModel))]
        public IActionResult SaveManufacturer(ManufacturerModel manufacturerModel)
        {
            var manufacturer = manufacturerModel.Id > 0 ? _manufacturerService.Get(manufacturerModel.Id) : new Manufacturer();
            if (manufacturer == null)
                return NotFound();

            manufacturer.Name = manufacturerModel.Name;
            _manufacturerService.InsertOrUpdate(manufacturer);
            return R.Success.With("id", manufacturer.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteManufacturer)]
        [CapabilityRequired(CapabilitySystemNames.EditManufacturer)]
        public IActionResult DeleteManufacturer(int manufacturerId)
        {
            var manufacturer = manufacturerId > 0 ? _manufacturerService.Get(manufacturerId) : new Manufacturer();
            if (manufacturer == null)
                return NotFound();

            _manufacturerService.Delete(manufacturer);
            return R.Success.Result;
        }
    }
}