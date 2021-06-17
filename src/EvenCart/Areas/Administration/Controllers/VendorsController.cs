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
using EvenCart.Areas.Administration.Factories.Users;
using EvenCart.Areas.Administration.Models.Vendors;
using EvenCart.Events;
using EvenCart.Genesis.Mvc;
using Genesis;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Data;
using Genesis.Modules.Users;
using Genesis.Modules.Vendors;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class VendorsController : GenesisAdminController
    {
        private readonly IVendorService _vendorService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly IRoleService _roleService;
        private readonly IUserModelFactory _userModelFactory;
        public VendorsController(IVendorService vendorService, IModelMapper modelMapper, IDataSerializer dataSerializer, IRoleService roleService, IUserModelFactory userModelFactory)
        {
            _vendorService = vendorService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _roleService = roleService;
            _userModelFactory = userModelFactory;
        }

        [DualGet("", Name = AdminRouteNames.VendorsList)]
        [CapabilityRequired(CapabilitySystemNames.ViewVendors)]
        public IActionResult VendorsList([FromQuery]VendorSearchModel searchModel)
        {
            var vendors = _vendorService.GetVendors( out var totalMatches, searchModel.SearchPhrase, searchModel.UserId, searchModel.VendorStatus, searchModel.Current, searchModel.RowCount);
            var vendorModels = vendors.Select(x => _modelMapper.Map<VendorModel>(x)).ToList();
            return R.Success.With("vendors", vendorModels)
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveVendor)]
        [CapabilityRequired(CapabilitySystemNames.EditVendor)]
        [ValidateModelState(ModelType = typeof(VendorModel))]
        public IActionResult SaveVendor(VendorModel vendorModel)
        {
            var vendor = vendorModel.Id > 0 ? _vendorService.Get(vendorModel.Id) : new Vendor()
            {
                VendorStatus = VendorStatus.Pending
            };
            if (vendor == null)
                return NotFound();
            vendor.Address = vendorModel.Address;
            vendor.City = vendorModel.City;
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
            vendor.VendorStatus = vendorModel.VendorStatus;
            _vendorService.InsertOrUpdate(vendor);

            if (vendor.VendorStatus != vendorModel.VendorStatus && vendorModel.SendNotification)
            {
                switch (vendor.VendorStatus)
                {
                    case VendorStatus.Active:
                        if (vendor.Users?.Any() ?? false)
                        {
                            foreach (var user in vendor.Users)
                            {
                                _roleService.SetUserRole(user.Id, SystemRoleNames.Vendor);
                            }
                        }
                        RaiseEvent(NamedEvent.VendorActivated, CurrentUser, vendor);
                        break;
                    case VendorStatus.Inactive:
                        RaiseEvent(NamedEvent.VendorDeactivated, CurrentUser, vendor);
                        break;
                    case VendorStatus.Rejected:
                        RaiseEvent(NamedEvent.VendorRejected, CurrentUser, vendor);
                        break;
                }
            }

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
            var availableVendorStatus = SelectListHelper.GetSelectItemList<VendorStatus>();
            return R.Success.With("vendor", vendorModel).WithAvailableCountries()
                .With("vendorId", vendorId)
                .With("availableVendorStatus", availableVendorStatus).Result;
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

        [DualGet("{vendorId}/users", Name = AdminRouteNames.VendorUsersList)]
        [CapabilityRequired(CapabilitySystemNames.ViewVendors)]
        public IActionResult VendorUsers(int vendorId)
        {
            if (vendorId == 0)
            {
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;
            }
            var vendor = vendorId > 0 ? _vendorService.Get(vendorId) : null;
            if (vendor == null)
            {
                return NotFound();
            }

            var userModels = vendor.Users.Select(_userModelFactory.CreateMini).ToList();
            return R.Success.WithGridResponse(userModels.Count, 1, userModels.Count).With("users", userModels).Result;
        }

        [DualPost("{vendorId}/users", Name = AdminRouteNames.SaveVendorUser, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditVendor)]
        public IActionResult SaveVendorUser(int vendorId, int[] userIds)
        {
            if (userIds == null || !userIds.Any())
                return BadRequest();
            var vendor = vendorId > 0 ? _vendorService.Get(vendorId) : null;
            if (vendor == null)
            {
                return NotFound();
            }

            foreach (var userId in userIds)
                _vendorService.AddVendorUser(vendorId, userId);
            return R.Success.Result;
        }

        [DualPost("{vendorId}/users/delete", Name = AdminRouteNames.DeleteVendorUser, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditVendor)]
        public IActionResult DeleteVendorUser(int vendorId, int userId)
        {
            var vendor = vendorId > 0 ? _vendorService.Get(vendorId) : null;
            if (vendor == null)
            {
                return NotFound();
            }

            _vendorService.RemoveVendorUser(vendorId, userId);
            return R.Success.Result;
        }
    }
}