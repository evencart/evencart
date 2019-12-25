using System.Linq;
using EvenCart.Data.Entity.Users;
using EvenCart.Events;
using EvenCart.Factories.Vendors;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Vendors;
using EvenCart.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    [Route("~/vendors")]
    [Authorize]
    public class VendorsController : FoundationController
    {
        private readonly IVendorService _vendorService;
        private readonly IVendorModelFactory _vendorModelFactory;
        public VendorsController(IVendorService vendorService, IVendorModelFactory vendorModelFactory)
        {
            _vendorService = vendorService;
            _vendorModelFactory = vendorModelFactory;
        }

        [HttpGet("register", Name = RouteNames.VendorRegister)]
        public IActionResult Register()
        {
            return R.Success.WithAvailableCountries().Result;
        }
        [DualPost("", Name = RouteNames.SaveVendor, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(VendorModel))]
        public IActionResult SaveVendor(VendorModel vendorModel)
        {
            var vendor = vendorModel.Id > 0 ? _vendorService.Get(vendorModel.Id) : new Vendor();
            if (vendor == null)
                return NotFound();
            vendor.Address = vendorModel.Address;
            vendor.Email = vendorModel.Email;
            vendor.Name = vendorModel.Name;
            vendor.GstNumber = vendorModel.GstNumber;
            vendor.CountryId = vendorModel.CountryId;
            vendor.Pan = vendorModel.Pan;
            vendor.StateProvinceId = vendorModel.StateProvinceId;
            vendor.StateProvinceName = vendorModel.StateProvinceName;
            vendor.Tin = vendorModel.Tin;
            vendor.ZipPostalCode = vendorModel.ZipPostalCode;
            vendor.City = vendorModel.City;
            vendor.Phone = vendorModel.Phone;
            vendor.VendorStatus = VendorStatus.Pending;
            _vendorService.InsertOrUpdate(vendor);

            //set vendor user
            _vendorService.AddVendorUser(vendor.Id, CurrentUser.Id);

            RaiseEvent(NamedEvent.VendorRegistered, CurrentUser, vendor);
            return R.Success.Result;
        }

        [DualGet("~/account/vendors", Name = RouteNames.VendorsList)]
        public IActionResult VendorsList()
        {
            var vendors = _vendorService.GetVendors(out _, null, CurrentUser.Id);
            var models = vendors.Select(_vendorModelFactory.Create).ToList();
            return R.Success.With("vendors", models).Result;
        }

        [DualGet("{vendorId}", Name = RouteNames.GetVendor)]
        public IActionResult VendorEditor(int vendorId)
        {
            var vendor = vendorId > 0 ? _vendorService.Get(vendorId) : null;
            if (vendor == null || vendor.Users.All(x => x.Id != CurrentUser.Id))
                return NotFound();

            var vendorModel = _vendorModelFactory.Create(vendor);
            return R.Success.With("vendor", vendorModel).WithAvailableCountries().Result;
        }
    }
}