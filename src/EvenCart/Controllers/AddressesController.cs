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
using EvenCart.Genesis.Mvc;
using EvenCart.Models.Addresses;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Addresses;
using Genesis.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows users to manage addresses
    /// </summary>
    [Route("addresses")]
    [Authorize]
    public class AddressesController : GenesisController
    {
        private readonly IAddressService _addressService;
        private readonly IModelMapper _modelMapper;
        public AddressesController(IAddressService addressService, IModelMapper modelMapper)
        {
            _addressService = addressService;
            _modelMapper = modelMapper;
        }
        /// <summary>
        /// Saves an address to the database
        /// </summary>
        /// <param name="addressModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("", Name = RouteNames.SaveAddress, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(AddressInfoModel))]
        public IActionResult SaveAddress(AddressInfoModel addressModel)
        {
            var currentUser = Engine.CurrentUser;
            //get the address first
            var address = addressModel.Id > 0 ? _addressService.Get(addressModel.Id) : new Address()
            {
                EntityId = currentUser.Id,
                EntityName = nameof(User)
            };
            if (address == null || address.EntityId != currentUser.Id)
                return NotFound();

            _modelMapper.Map(addressModel, address);
            _addressService.InsertOrUpdate(address);
            return R.Success.Result;
        }
        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="addressId">The identifier of the address to be deleted</param>
        /// <response code="200">A success response object</response>
        [DualPost("{addressId}", Name = RouteNames.DeleteAddress, OnlyApi = true)]
        public IActionResult DeleteAddress(int addressId)
        {
            var currentUser = Engine.CurrentUser;
            //get the address first
            var address = addressId > 0 ? _addressService.Get(addressId) : null;
            if (address?.EntityName != nameof(User) || address.EntityId != currentUser.Id)
                return NotFound();

            _addressService.Delete(address);
            return R.Success.Result;
        }
        /// <summary>
        /// Gets the addresses of authenticated user
        /// </summary>
        /// <response code="200">A list of <see cref="AddressInfoModel">addresses</see></response>
        [DualGet("~/account/addresses", Name = RouteNames.AccountAddresses)]
        public IActionResult Addresses()
        {
            var currentUser = Engine.CurrentUser;
            var addresses = _addressService.Get(x => x.EntityId == currentUser.Id && x.EntityName == nameof(User)).ToList();
            var models = addresses.Select(x =>
            {
                var model = _modelMapper.Map<AddressInfoModel>(x);
                model.CountryName = x.Country.Name;
                model.StateProvinceName = x.StateOrProvince?.Name ?? x.StateProvinceName;
                return model;
            }).ToList();
            return R.Success.With("addresses", models).Result;
        }

        /// <summary>
        /// Gets a single address for authenticated user
        /// </summary>
        /// <param name="addressId">The id of the address to retrieve</param>
        /// <response code="200">The <see cref="AddressInfoModel">address</see> object along with availableCountries and availableAddressTypes as <see cref="SelectListItem">items</see> lists.</response>
        [DualGet("{addressId}", Name = RouteNames.SingleAddress)]
        public IActionResult AddressEditor(int addressId)
        {
            var currentUser = Engine.CurrentUser;
            //find address
            var address = addressId > 0 ? _addressService.Get(addressId) : new Address()
            {
                EntityId = currentUser.Id
            };
            //only allow if current user can edit this address
            if (address == null || address.EntityId != Engine.CurrentUser.Id)
                return NotFound();
            var model = _modelMapper.Map<AddressInfoModel>(address);
            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Addresses", RouteNames.AccountAddresses);
            SetBreadcrumbToRoute("Edit Address", RouteNames.SingleAddress);
            return R.Success.With("address", model).WithAvailableCountries().WithAvailableAddressTypes().Result;
        }
    }
}