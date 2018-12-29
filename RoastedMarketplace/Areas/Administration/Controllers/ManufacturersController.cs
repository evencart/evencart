using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Manufacturers;
using RoastedMarketplace.Areas.Administration.Models.Shop;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Products;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
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
            return R.Success.With("manufacturers", () => manufacturersModel,
                    () => _dataSerializer.Serialize(manufacturersModel))
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