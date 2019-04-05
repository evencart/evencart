using System;
using System.Collections.Generic;
using System.Linq;
using DotEntity.Enumerations;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Factories.Orders;
using RoastedMarketplace.Areas.Administration.Models.Orders;
using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Data.Entity.Cultures;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Factories.Users;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Addresses;
using RoastedMarketplace.Services.Formatter;
using RoastedMarketplace.Services.Purchases;
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
        private readonly IOrderService _orderService;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IRoleModelFactory _roleModelFactory;
        private readonly ICartService _cartService;
        public UsersController(IUserService userService, IModelMapper modelMapper, IRoleService roleService, ICapabilityService capabilityService, IUserRegistrationService userRegistrationService, IDataSerializer dataSerializer, IAddressService addressService, IOrderService orderService, IOrderModelFactory orderModelFactory, IRoleModelFactory roleModelFactory, ICartService cartService)
        {
            _userService = userService;
            _modelMapper = modelMapper;
            _roleService = roleService;
            _capabilityService = capabilityService;
            _userRegistrationService = userRegistrationService;
            _dataSerializer = dataSerializer;
            _addressService = addressService;
            _orderService = orderService;
            _orderModelFactory = orderModelFactory;
            _roleModelFactory = roleModelFactory;
            _cartService = cartService;
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

        [DualGet("{userId}/orders", Name = AdminRouteNames.UserOrdersList)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult OrdersList(int userId, OrderSearchModel orderSearchModel)
        {
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var orders = _orderService.Get(out int totalResults, x => x.UserId == userId, x => x.CreatedOn,
                RowOrder.Descending, orderSearchModel.Current, orderSearchModel.RowCount).ToList();
            var ordersModel = orders.Select(_orderModelFactory.Create).ToList();
            return R.Success.With("orders", ordersModel)
                .WithGridResponse(totalResults, orderSearchModel.Current, orderSearchModel.RowCount).Result;
        }

        [DualGet("{userId}/capabilities", Name = AdminRouteNames.CapabilitiesList)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult CapabilitiesList(int userId)
        {
            User user = null;
            if (userId <= 0 || (user = _userService.Get(userId)) == null)
                return NotFound();

            var roleIds = user.Roles.Select(x => x.Id).ToArray();
            var roleCapabilities = _capabilityService.GetByRolesConsolidated(roleIds).OrderBy(x => x.Name);
            var userCapabilities = _capabilityService.GetByUser(userId);
            var allCapabilities = _capabilityService.Get(out int _, x => true, x => x.Name);
            var availableCapabilities = allCapabilities.Where(x => roleCapabilities.All(y => y.Id != x.Id)).ToList();

            var activeCapabilityModels = roleCapabilities.Select(_roleModelFactory.Create);

            var availableCapabilitiesModel = availableCapabilities.Select(x =>
            {
                var model = _roleModelFactory.Create(x);
                model.Active = userCapabilities?.Any(y => y.Id == x.Id) ?? false;
                return model;
            }).ToList();
            var roleModels = user.Roles.Select(_roleModelFactory.Create);
            return R.Success.With("capabilities", activeCapabilityModels)
                .With("availableCapabilities", availableCapabilitiesModel)
                .With("roles", roleModels).Result;

        }

        [DualPost("{userId}/capabilities", Name = AdminRouteNames.SaveCapabilities)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult SaveCapabilities(int userId, IList<int> capabilityIds)
        {
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            _capabilityService.SetUserCapabilities(userId, capabilityIds.ToArray());
            return R.Success.Result;
        }

        [DualGet("{userId}/cart", Name = AdminRouteNames.UserCart)]
        [CapabilityRequired(CapabilitySystemNames.ManageCart)]
        public IActionResult UserCart(int userId)
        {
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var formatterService = DependencyResolver.Resolve<IFormatterService>();
            var cart = _cartService.GetCart(userId);
            var models = cart.CartItems.Select(x =>
            {

                var cartItem = new CartItemModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    Price = x.Price + x.Tax,
                    Quantity = x.Quantity,
                    Discount = x.Discount,
                    Tax = x.Tax,
                    TaxPercent = x.TaxPercent,
                    ImageUrl = mediaAccountant.GetPictureUrl(x.Product.MediaItems?.FirstOrDefault(), ApplicationEngine.ActiveTheme.CartItemImageSize, true),
                    Slug = x.Product.SeoMeta.Slug,
                    AttributeText = formatterService.FormatProductAttributes(x.AttributeJson)
                };
                cartItem.SubTotal = cartItem.Price * cartItem.Quantity;
                cartItem.FinalPrice = cartItem.SubTotal + cartItem.Tax - cartItem.Discount;
                return cartItem;
            }).ToList();
            return R.Success.With("cartItems", models).WithGridResponse(models.Count, 1, models.Count).Result;
        }

        [HttpGet("{userId}/imitate", Name = AdminRouteNames.UserImitate)]
        public IActionResult Imitate(int userId)
        {
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