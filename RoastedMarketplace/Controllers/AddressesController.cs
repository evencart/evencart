using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Addresses;
using RoastedMarketplace.Services.Addresses;

namespace RoastedMarketplace.Controllers
{
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
    }
}