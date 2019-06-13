using System.Linq;
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Addresses;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Allows store admin to manage warehouses
    /// </summary>
    public class WarehouseController : FoundationAdminController
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IWarehouseModelFactory _warehouseModelFactory;
        private readonly IAddressService _addressService;
        private readonly IModelMapper _modelMapper;
        private readonly CatalogSettings _catalogSettings;
        public WarehouseController(IWarehouseService warehouseService, IWarehouseModelFactory warehouseModelFactory, IAddressService addressService, IModelMapper modelMapper, CatalogSettings catalogSettings)
        {
            _warehouseService = warehouseService;
            _warehouseModelFactory = warehouseModelFactory;
            _addressService = addressService;
            _modelMapper = modelMapper;
            _catalogSettings = catalogSettings;
        }

        /// <summary>
        /// Get the ware house list
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="WarehouseModel">warehouse</see> objects as 'warehouses'</response>
        [DualGet("", Name = AdminRouteNames.WarehouseList)]
        [CapabilityRequired(CapabilitySystemNames.ManageWarehouses)]
        public IActionResult WarehouseList(WarehouseSearchModel searchModel)
        {
            var warehouses = _warehouseService.Get(out int totalResults, x => true, x => x.DisplayOrder, page: searchModel.Current, count: searchModel.RowCount).ToList();
            var models = warehouses.Select(_warehouseModelFactory.Create).ToList();
            return R.Success.With("warehouses", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;
        }

        /// <summary>
        /// Gets a warehouse with specific id
        /// </summary>
        /// <param name="warehouseId">The id of the warehouse</param>
        /// <response code="200">A <see cref="WarehouseModel">warehouse</see> object as 'warehouse'</response>
        [DualGet("{warehouseId}", Name = AdminRouteNames.GetWarehouse)]
        [CapabilityRequired(CapabilitySystemNames.ManageWarehouses)]
        public IActionResult WarehouseEditor(int warehouseId)
        {
            var warehouse = warehouseId > 0 ? _warehouseService.Get(warehouseId) : new Warehouse();
            var model = _warehouseModelFactory.Create(warehouse);
            return R.Success.With("warehouseId", warehouseId).With("warehouse", model).WithAvailableCountries().WithAvailableAddressTypes().Result;
        }

        /// <summary>
        /// Saves a warehouse to database
        /// </summary>
        /// <param name="warehouseModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("", Name = AdminRouteNames.SaveWarehouse, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageWarehouses)]
        [ValidateModelState(ModelType = typeof(WarehouseModel))]
        public IActionResult SaveWarehouse(WarehouseModel warehouseModel)
        {
            var warehouse = warehouseModel.Id > 0 ? _warehouseService.Get(warehouseModel.Id) : new Warehouse()
            {
                Address = new Address()
            };
            if (warehouse == null)
                return NotFound();
            var address = warehouse.Address;
            _modelMapper.Map(warehouseModel.Address, address, "Id");
            address.EntityName = nameof(Warehouse);
            _addressService.InsertOrUpdate(address);
            //save warehouse
            warehouse.AddressId = address.Id;
            _warehouseService.InsertOrUpdate(warehouse);
            return R.Success.Result;
        }

        /// <summary>
        /// Deletes specific warehouse
        /// </summary>
        /// <param name="warehouseId">The id of the warehouse</param>
        /// <response code="200">A success response object</response>
        [DualPost("delete", Name = AdminRouteNames.DeleteWarehouse, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageWarehouses)]
        public IActionResult DeleteWarehouse(int warehouseId)
        {
            if (_warehouseService.Count() == 1)
                return R.Fail.With("error", T("At least one warehouse must be there for inventory calculation")).Result;
            var warehouse = _warehouseService.Get(warehouseId);
            if (warehouse == null)
                return NotFound();
            
            _warehouseService.Delete(warehouse);
            return R.Success.Result;
        }

        /// <summary>
        /// Updates display order for warehouses
        /// </summary>
        /// <param name="warehouseModels"></param>
        /// <response code="200">A success response object</response>
        [DualPost("display-order", Name = AdminRouteNames.UpdateWarehouseDisplayOrder, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageWarehouses)]
        public IActionResult UpdateWarehouseDisplayOrder(WarehouseModel[] warehouseModels)
        {
            if (warehouseModels == null)
                return BadRequest();
            //get category models with no-zero ids
            var validModels = warehouseModels.Where(x => x.Id != 0);
            Transaction.Initiate(transaction =>
            {
                foreach (var model in validModels)
                {
                    _warehouseService.Update(new { DisplayOrder = model.DisplayOrder }, m => m.Id == model.Id,
                        transaction);
                }
            });
            return R.Success.Result;
        }
    }
}