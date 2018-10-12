using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Addresses;
using RoastedMarketplace.Services.Serializers;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class UsersController : FoundationAdminController
    {
        private readonly IUserService _userService;
        private readonly IModelMapper _modelMapper;
        private readonly IRoleService _roleService;
        private readonly ICapabilityService _capabilityService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IAddressService _addressService;
        public UsersController(IUserService userService, IModelMapper modelMapper, IRoleService roleService, ICapabilityService capabilityService, IUserRegistrationService userRegistrationService, IDataSerializer dataSerializer, IAddressService addressService)
        {
            _userService = userService;
            _modelMapper = modelMapper;
            _roleService = roleService;
            _capabilityService = capabilityService;
            _userRegistrationService = userRegistrationService;
            _dataSerializer = dataSerializer;
            _addressService = addressService;
        }

        [DualGet("", Name = AdminRouteNames.UsersList)]
        [ValidateModelState(ModelType = typeof(UserSearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewUsers)]
        public IActionResult UsersList([FromQuery] UserSearchModel searchModel)
        {
            var users = _userService.GetUsers(searchModel.SearchPhrase, searchModel.RoleIds, searchModel.Current,
                searchModel.RowCount, out int totalMatches);

            //convert to model
            var userModels = users.Select(x =>
            {
                var userModel = _modelMapper.Map<UserModel>(x);
                userModel.Roles = x.Roles?.Select(y => _modelMapper.Map<RoleModel>(y)).ToList();
                return userModel;
            }).ToList();

            var roles = _roleService.Get(x => true);
            var roleModels = roles.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            return R.Success.With("users", () => userModels, () => _dataSerializer.Serialize(userModels))
                .With("roles", roleModels)
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("{userId}", Name = AdminRouteNames.GetUser)]
        [CapabilityRequired(CapabilitySystemNames.ViewUsers)]
        public IActionResult UserEditor(int userId)
        {
            var user = userId > 0 ? _userService.Get(userId) : new User();
            if (user == null)
                return NotFound();

            var userModel = _modelMapper.Map<UserModel>(user);

            var roles = _roleService.Get(x => true).ToList();
            var roleModels = roles.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();

            //set default role to registered
            if (user.Id == 0)
            {
                userModel.Roles = roles.Where(x => x.SystemName == SystemRoleNames.Registered).Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            }
            else
            {
                userModel.Roles = user.Roles?.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();

            }
            return R.Success.With("user", userModel).With("roles", roleModels).Result;
        }

        [DualPost("", Name = AdminRouteNames.SaveUser, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        [ValidateModelState(ModelType = typeof(UserModel))]
        public IActionResult SaveUser(UserModel userModel)
        {
            var user = userModel.Id > 0 ? _userService.FirstOrDefault(x => x.Id == userModel.Id) : new User();
            if (user == null)
                return NotFound();
            user = _modelMapper.Map(userModel, user,
                excludeProperties: new[]
                    {nameof(user.DateCreated), nameof(user.DateUpdated), nameof(user.LastLoginDate)});
            if (user.Id == 0)
            {
                user.Guid = Guid.NewGuid();
                user.DateCreated = DateTime.UtcNow;
                user.DateUpdated = DateTime.UtcNow;
                _userRegistrationService.Register(user, ApplicationConfig.DefaultPasswordFormat);
            }
            else
                _userService.Update(user);

            //get the role ids
            var roleIds = userModel.Roles?.Select(x => x.Id).ToArray() ?? null;
            _roleService.SetUserRoles(user.Id, roleIds);

            return R.Success.With("id", user.Id).Result;
        }

        [DualPost("delete", Name = AdminRouteNames.DeleteUser, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.DeleteUser)]
        public IActionResult DeleteUser(int userId)
        {
            var user = _userService.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return NotFound();

            _userService.Delete(user);
            return R.Success.Result;
        }

        [DualGet("{userId}/addresses", Name = AdminRouteNames.AddressList)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult AddressList(int userId)
        {
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var addresses = _addressService.Get(x => x.UserId == userId).ToList();
            var addressesModel = addresses.Select(x =>
            {
                var aModel = MapAddressModel(x);
                aModel.CountryName = x.Country.Name;
                return aModel;
            }).ToList();
            return R.Success.With("addresses", () => addressesModel, () => _dataSerializer.Serialize(addressesModel)).Result;
        }

        [DualGet("{userId}/addresses/{addressId}", Name = AdminRouteNames.GetAddress)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult AddressEditor(int userId, int addressId)
        {
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var address = addressId > 0 ? _addressService.Get(addressId) : new Address();
            if (address == null)
                return NotFound();

            var addressModel = _modelMapper.Map<AddressModel>(address);
            addressModel.UserId = userId;
            return R.Success.With("address", addressModel).WithAvailableCountries().WithAvailableAddressTypes().Result;
        }

        [DualPost("addresses", Name = AdminRouteNames.SaveAddress, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        [ValidateModelState(ModelType = typeof(AddressModel))]
        public IActionResult SaveAddress(AddressModel addressModel)
        {
            var address = addressModel.Id > 0 ? _addressService.Get(addressModel.Id) : new Address();
            if (address == null)
                return NotFound();
            _modelMapper.Map(addressModel, address);
            _addressService.InsertOrUpdate(address);
            return R.Success.Result;
        }

        [DualPost("addresses/delete", Name = AdminRouteNames.DeleteAddress, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult DeleteAddress(int addressId)
        {
            var address = _addressService.Get(addressId);
            if (address == null)
                return NotFound();
            _addressService.Delete(address);
            return R.Success.Result;
        }
        #region Helpers

        private AddressModel MapAddressModel(Address address)
        {
            var addressModel = _modelMapper.Map<AddressModel>(address);
            addressModel.CountryName = address.Country.Name;
            return addressModel;
        }
        #endregion
    }
}