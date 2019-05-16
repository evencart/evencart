using System.Linq;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Services.Addresses;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Addresses;
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
    public class AddressesController : FoundationController
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
            var currentUser = ApplicationEngine.CurrentUser;
            //get the address first
            var address = addressModel.Id > 0 ? _addressService.Get(addressModel.Id) : new Address()
            {
                UserId = currentUser.Id
            };
            if (address == null || address.UserId != currentUser.Id)
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
            var currentUser = ApplicationEngine.CurrentUser;
            //get the address first
            var address = addressId > 0 ? _addressService.Get(addressId) : null;
            if (address == null || address.UserId != currentUser.Id)
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
            var currentUser = ApplicationEngine.CurrentUser;
            var addresses = _addressService.Get(x => x.UserId == currentUser.Id).ToList();
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
            var currentUser = ApplicationEngine.CurrentUser;
            //find address
            var address = addressId > 0 ? _addressService.Get(addressId) : new Address()
            {
                UserId = currentUser.Id
            };
            //only allow if current user can edit this address
            if (address == null || address.UserId != ApplicationEngine.CurrentUser.Id)
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