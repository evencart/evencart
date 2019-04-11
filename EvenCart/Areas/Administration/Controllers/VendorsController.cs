using System.Linq;
using EvenCart.Areas.Administration.Models.Vendors;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Serializers;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class VendorsController : FoundationAdminController
    {
        private readonly IVendorService _vendorService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;

        public VendorsController(IVendorService vendorService, IModelMapper modelMapper, IDataSerializer dataSerializer)
        {
            _vendorService = vendorService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
        }

        [DualGet("", Name = AdminRouteNames.VendorsList)]
        [CapabilityRequired(CapabilitySystemNames.ViewVendors)]
        public IActionResult VendorsList([FromQuery]VendorSearchModel searchModel)
        {
            var vendors = _vendorService.GetVendors(searchModel.SearchPhrase, searchModel.Current, searchModel.RowCount,
                out int totalMatches);
            var vendorModels = vendors.Select(x => _modelMapper.Map<VendorModel>(x)).ToList();
            return R.Success.With("vendors", () => vendorModels, () => _dataSerializer.Serialize(vendorModels))
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveVendor)]
        [CapabilityRequired(CapabilitySystemNames.EditVendor)]
        [ValidateModelState(ModelType = typeof(VendorModel))]
        public IActionResult SaveVendor(VendorModel vendorModel)
        {
            var vendor = vendorModel.Id > 0 ? _vendorService.Get(vendorModel.Id) : new Vendor();
            if (vendor == null)
                return NotFound();
            _modelMapper.Map(vendorModel, vendor);
            _vendorService.InsertOrUpdate(vendor);
            return R.Success.Result;
        }

        [DualGet("{vendorId}", Name = AdminRouteNames.GetVendor)]
        [CapabilityRequired(CapabilitySystemNames.EditVendor)]
        public IActionResult VendorEditor(int vendorId)
        {
            var vendor = vendorId > 0 ? _vendorService.Get(vendorId) : new Vendor();
            if (vendor == null)
            {
                return NotFound();
            }
            var vendorModel = _modelMapper.Map<VendorModel>(vendor);
            return R.Success.With("vendor", vendorModel).WithAvailableCountries().Result;
        }

        [DualPost("{vendorId}", Name = AdminRouteNames.DeleteVendor)]
        [CapabilityRequired(CapabilitySystemNames.DeleteVendor)]
        public IActionResult DeleteVendor(int vendorId)
        {

            Vendor vendor = null;
            if (vendorId <= 0 || (vendor = _vendorService.Get(vendorId)) == null)
            {
                return NotFound();
            }
            _vendorService.Delete(vendor);
            return R.Success.Result;
        }
    }
}