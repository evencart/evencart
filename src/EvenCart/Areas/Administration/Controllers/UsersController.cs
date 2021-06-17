﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DotEntity.Enumerations;
using EvenCart.Areas.Administration.Factories.Addresses;
using EvenCart.Areas.Administration.Factories.Orders;
using EvenCart.Areas.Administration.Factories.Users;
using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Events;
using EvenCart.Genesis.Mvc;
using EvenCart.Services.Orders;
using Genesis;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.MediaServices;
using Genesis.Modules.Addresses;
using Genesis.Modules.Data;
using Genesis.Modules.Stores;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    /// <summary>
    /// Allows admins to manage users
    /// </summary>
    public class UsersController : GenesisAdminController
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
        private readonly IUserCodeService _userCodeService;
        private readonly IInviteRequestService _inviteRequestService;
        private readonly IAddressModelFactory _addressModelFactory;
        private readonly IUserPointService _userPointService;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IStoreCreditService _storeCreditService;
        public UsersController(IUserService userService, IModelMapper modelMapper, IRoleService roleService, ICapabilityService capabilityService, IUserRegistrationService userRegistrationService, IDataSerializer dataSerializer, IAddressService addressService, IOrderService orderService, IOrderModelFactory orderModelFactory, IRoleModelFactory roleModelFactory, ICartService cartService, IUserCodeService userCodeService, IInviteRequestService inviteRequestService, IAddressModelFactory addressModelFactory, IUserPointService userPointService, IUserModelFactory userModelFactory, IStoreCreditService storeCreditService)
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
            _userCodeService = userCodeService;
            _inviteRequestService = inviteRequestService;
            _addressModelFactory = addressModelFactory;
            _userPointService = userPointService;
            _userModelFactory = userModelFactory;
            _storeCreditService = storeCreditService;
        }

        [DualGet("", Name = AdminRouteNames.UsersList)]
        [ValidateModelState(ModelType = typeof(UserSearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewUsers)]
        public IActionResult UsersList([FromQuery] UserSearchModel searchModel)
        {
            var negate = searchModel.RoleIds == null;
            searchModel.RoleIds = searchModel.RoleIds ?? new int[]
            {
                _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Visitor)?.Id ?? 0
            };
            var users = _userService.GetUsers(searchModel.SearchPhrase, searchModel.RoleIds, null, SortOrder.Ascending, searchModel.Current,
                searchModel.RowCount, out int totalMatches, negate);

            //convert to model
            var userModels = users.Select(x =>
            {
                var userModel = _modelMapper.Map<UserModel>(x);
                userModel.Roles = x.Roles?.Select(y => _modelMapper.Map<RoleModel>(y)).ToList();
                return userModel;
            }).ToList();

            var roles = _roleService.Get(x => true);
            var roleModels = roles.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            return R.Success.With("users", userModels)
                .With("roles", roleModels)
                .WithGridResponse(totalMatches, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("affiliates", Name = AdminRouteNames.AffiliatesList)]
        [ValidateModelState(ModelType = typeof(UserSearchModel))]
        [CapabilityRequired(CapabilitySystemNames.ViewUsers)]
        public IActionResult AffiliatesList([FromQuery] UserSearchModel searchModel)
        {
            var negate = searchModel.RoleIds == null;
            searchModel.RoleIds = searchModel.RoleIds ?? new int[]
            {
                _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Visitor)?.Id ?? 0
            };
            var users = _userService.GetUsers(searchModel.SearchPhrase, searchModel.RoleIds, null, SortOrder.Ascending, searchModel.Current,
                searchModel.RowCount, out int totalMatches, negate, x => x.IsAffiliate);

            //convert to model
            var userModels = users.Select(x =>
            {
                var userModel = _modelMapper.Map<UserModel>(x);
                userModel.Roles = x.Roles?.Select(y => _modelMapper.Map<RoleModel>(y)).ToList();
                return userModel;
            }).ToList();

            var roles = _roleService.Get(x => true);
            var roleModels = roles.Select(x => _modelMapper.Map<RoleModel>(x)).ToList();
            return R.Success.With("users", userModels)
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
            user.Active = userModel.Active;
            user.CompanyName = userModel.CompanyName;
            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.IsTaxExempt = userModel.IsTaxExempt;
            user.DateOfBirth = userModel.DateOfBirth;
            user.MobileNumber = userModel.MobileNumber;
            user.NewslettersEnabled = userModel.NewslettersEnabled;
            user.Remarks = userModel.Remarks;
            user.RequirePasswordChange = userModel.RequirePasswordChange;
            user.Name = $"{user.FirstName} {user.LastName}";
            user.IsAffiliate = userModel.IsAffiliate;
            user.AffiliateActive = userModel.AffiliateActive;
            var firstActivation = user.Active && user.FirstActivationDate == null;
            if (firstActivation)
            {
                user.FirstActivationDate = DateTime.UtcNow;
            }
            if (user.AffiliateFirstActivationDate == null && userModel.AffiliateActive)
            {
                user.AffiliateFirstActivationDate = DateTime.UtcNow;
            }
            if (user.Id == 0)
            {
                user.Guid = Guid.NewGuid();
                user.CreatedOn = DateTime.UtcNow;
                user.UpdatedOn = DateTime.UtcNow;
                user.Password = userModel.Password;
                _userRegistrationService.Register(user, Engine.StaticConfig.DefaultPasswordFormat);
            }
            else
            {
               _userService.Update(user);
               //update password if so
               if (!userModel.Password.IsNullEmptyOrWhiteSpace())
               {
                   _userRegistrationService.UpdatePassword(user.Id, userModel.Password, Engine.StaticConfig.DefaultPasswordFormat);
                   RaiseEvent(NamedEvent.PasswordReset, user, userModel.Password);
                }
            }

            //get the role ids
            var roleIds = userModel.Roles?.Select(x => x.Id).ToArray() ?? null;
            _roleService.SetUserRoles(user.Id, roleIds, true);
            if (firstActivation)
            {
                RaiseEvent(NamedEvent.UserActivated, user);
            }
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
            if(userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId < 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var addresses = _addressService.Get(x => x.EntityId == userId && x.EntityName == nameof(User)).ToList();
            var addressesModel = addresses.Select(_addressModelFactory.Create).ToList();
            return R.Success.With("addresses", addressesModel).Result;
        }

        [DualGet("{userId}/addresses/{addressId}", Name = AdminRouteNames.GetAddress)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult AddressEditor(int userId, int addressId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId < 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var address = addressId > 0 ? _addressService.Get(addressId) : new Address()
            {
                EntityName = nameof(User),
                EntityId = userId
            };
            if (address?.EntityName != nameof(User) || address.EntityId != userId)
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
            address.EntityName = nameof(User);
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
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId < 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var orders = _orderService.Get(out int totalResults, x => x.UserId == userId, x => x.CreatedOn,
                RowOrder.Descending, orderSearchModel.Current, orderSearchModel.RowCount).ToList();
            var ordersModel = orders.Select(_orderModelFactory.Create).ToList();
            return R.Success.With("orders", ordersModel)
                .With("userId", userId)
                .WithGridResponse(totalResults, orderSearchModel.Current, orderSearchModel.RowCount).Result;
        }

        [DualGet("{userId}/capabilities", Name = AdminRouteNames.CapabilitiesList)]
        [CapabilityRequired(CapabilitySystemNames.EditUser)]
        public IActionResult CapabilitiesList(int userId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            User user = null;
            if (userId < 0 || (user = _userService.Get(userId)) == null)
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
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId < 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            var mediaAccountant = D.Resolve<IMediaAccountant>();
            var formatterService = D.Resolve<IFormatterService>();
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
                    ImageUrl = mediaAccountant.GetPictureUrl(x.Product.MediaItems?.FirstOrDefault(), Engine.ActiveTheme.CartItemImageSize, true),
                    Slug = x.Product.SeoMeta.Slug,
                    AttributeText = formatterService.FormatProductAttributes(x.AttributeJson)
                };
                cartItem.SubTotal = cartItem.Price * cartItem.Quantity;
                cartItem.FinalPrice = cartItem.SubTotal + cartItem.Tax - cartItem.Discount;
                return cartItem;
            }).ToList();
            return R.Success.With("cartItems", models).WithGridResponse(models.Count, 1, models.Count).Result;
        }

        [DualGet("{userId}/points", Name = AdminRouteNames.UserPointsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageUserPoints)]
        public IActionResult UserPointsList(UserPointSearchModel searchModel)
        {
            if (searchModel.UserId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            var userId = searchModel.UserId;
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            var userPoints = _userPointService.Get(out int totalResults, x => x.UserId == userId, x => x.Id, RowOrder.Descending, searchModel.Current, searchModel.RowCount);
            var pointsTotal = _userPointService.GetPoints(userId);
            var models = userPoints.Select(_userModelFactory.Create).ToList();
            return R.Success.With("userId", userId).With("userPoints", models).With("totalPoints", pointsTotal)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;
        }

        [DualGet("{userId}/points/{userPointId}", Name = AdminRouteNames.GetUserPoint)]
        [CapabilityRequired(CapabilitySystemNames.ManageUserPoints)]
        public IActionResult UserPointEditor(int userId, int userPointId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var userPoint = userPointId > 0 ? _userPointService.Get(userPointId) : new UserPoint()
            {
                UserId = userId,
                ActivatorUserId = CurrentUser.Id
            };
            if (userPoint == null)
                return NotFound();
            var model = _userModelFactory.Create(userPoint);
            return R.Success.With("userPoint", model).Result;
        }

        [DualPost("points", Name = AdminRouteNames.SaveUserPoint, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageUserPoints)]
        [ValidateModelState(ModelType = typeof(UserPointModel))]
        public IActionResult SaveUserPoint(UserPointModel userPointModel)
        {
            var id = userPointModel.Id;
            var userPoint = id > 0 ? _userPointService.Get(id) : new UserPoint()
            {
                ActivatorUserId = CurrentUser.Id,
                CreatedOn = DateTime.UtcNow,
                UserId = userPointModel.UserId
            };
        

            if (userPoint == null)
                return NotFound();
            userPoint.Points = userPointModel.Points;
            userPoint.Reason = userPointModel.Reason;
            _userPointService.InsertOrUpdate(userPoint);
            return R.Success.Result;
        }

        [HttpGet("{userId}/imitate", Name = AdminRouteNames.UserImitate)]
        [CapabilityRequired(CapabilitySystemNames.ImitateUser)]
        public IActionResult Imitate(int userId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            return R.Success.With("userId", userId).Result;
        }

        [HttpPost("{userId}/imitate", Name = AdminRouteNames.UserImitate)]
        [CapabilityRequired(CapabilitySystemNames.ImitateUser)]
        public IActionResult ImitatePost(int userId)
        {
            //can't imitate one's self
            if (CurrentUser.Id == userId)
                return R.Fail.WithView("Imitate").Result;

            User user;
            if (userId <= 0 || (user = _userService.FirstOrDefault(x => x.Id == userId)) == null)
                return NotFound();
            if (Engine.ImitationModeSignIn(user.Email) == LoginStatus.Success)
                return RedirectToRoute(RouteNames.Home);
            return R.Fail.WithView("Imitate").Result;
        }

        [HttpGet("{userId}/anonymize", Name = AdminRouteNames.AnonymizeUser)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdprPrivate)]
        public IActionResult Anonymize(int userId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            return R.Success.With("userId", userId).Result;
        }

        [DualPost("{userId}/anonymize", Name = AdminRouteNames.AnonymizeUser, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageGdprPrivate)]
        public IActionResult AnonymizePost(int userId)
        {
            //can't delete one's self
            if (CurrentUser.Id == userId)
                return R.Fail.WithView("Anonymize").Result;

            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            _userService.AnonymizeUser(userId);
            return R.Success.With("userId", userId).Result;
        }

        [DualGet("invitation-requests", Name = AdminRouteNames.InvitationRequestsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageInvitationRequests)]
        public IActionResult InvitationRequestsList(InvitationRequestsSearchModel searchModel)
        {
            searchModel = searchModel ?? new InvitationRequestsSearchModel();
            var requests = _inviteRequestService.Get(out int totalResults, x => true, page: searchModel.Current,
                count: searchModel.RowCount);
            var models = requests.Select(x => new InvitationRequestModel()
            {
                CreatedOn = x.CreatedOn,
                Email = x.Email,
                Accepted = x.Accepted,
                Id = x.Id
            }).ToList();
            return R.Success.With("invitationRequests", models)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;
        }

        [DualPost("invitation-requests", Name = AdminRouteNames.DeleteInvitationRequest, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageInvitationRequests)]
        public IActionResult DeleteInvitationRequest(int invitationRequestId)
        {
            var request = _inviteRequestService.Get(invitationRequestId);
            if (request == null)
            {
                return NotFound();
            }
            _inviteRequestService.Delete(request);
            return R.Success.Result;
        }

        [DualPost("generate-invite-link", Name = AdminRouteNames.GenerateInviteLink, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageInvitationRequests)]
        public IActionResult GenerateInviteLink(GenerateInvitationLinkModel generateModel)
        {
            //check if user with this email already exists
            var user = _userService.GetByUserInfo(generateModel.Email);
            if (user != null)
            {
                return R.Fail.With("error", T("A user with this email is already registered")).Result;
            }

            var userCode =  _userCodeService.GetUserCodeByEmail(generateModel.Email, UserCodeType.RegistrationInvitation);
            var invitationLink = Engine.RouteUrl(RouteNames.Register, new {invitationCode = userCode.Code}, true);
            RaiseEvent(NamedEvent.Invitation, userCode, invitationLink);
            return R.Success.Result;
        }

        [DualGet("{userId}/credits", Name = AdminRouteNames.StoreCreditsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageStoreCredits)]
        public IActionResult StoreCreditsList(StoreCreditSearchModel searchModel)
        {
            if (searchModel.UserId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            var userId = searchModel.UserId;
            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();
            var userCredits = _storeCreditService.Get(out int totalResults, x => x.UserId == userId, x => x.Id, RowOrder.Descending, searchModel.Current, searchModel.RowCount);
            var creditsTotal = _storeCreditService.GetBalance(userId);
            var models = userCredits.Select(_userModelFactory.Create).ToList();
            return R.Success.With("userId", userId).With("storeCredits", models).With("availableBalance", creditsTotal)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;
        }

        [DualGet("{userId}/credits/{storeCreditId}", Name = AdminRouteNames.GetStoreCredit)]
        [CapabilityRequired(CapabilitySystemNames.ManageStoreCredits)]
        public IActionResult StoreCreditEditor(int userId, int storeCreditId)
        {
            if (userId == 0)
                return R.Fail.WithError(ErrorCodes.ParentEntityMustBeNonZero).Result;

            if (userId <= 0 || _userService.Count(x => x.Id == userId) == 0)
                return NotFound();

            var userCredit = storeCreditId > 0 ? _storeCreditService.Get(storeCreditId) : new StoreCredit()
            {
                UserId = userId,
                AvailableOn = DateTime.UtcNow
            };
            if (userCredit == null)
                return NotFound();
            var model = _userModelFactory.Create(userCredit);
            return R.Success.With("storeCredit", model).Result;
        }

        [DualPost("credits", Name = AdminRouteNames.SaveStoreCredit, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageStoreCredits)]
        [ValidateModelState(ModelType = typeof(StoreCreditModel))]
        public IActionResult SaveStoreCredit(StoreCreditModel userCreditModel)
        {
            var id = userCreditModel.Id;
            var userCredit = id > 0 ? _storeCreditService.Get(id) : new StoreCredit()
            {
                CreatedOn = DateTime.UtcNow,
                UserId = userCreditModel.UserId
            };


            if (userCredit == null)
                return NotFound();
            userCredit.Credit = userCreditModel.Credit;
            userCredit.Description = userCreditModel.Description;
            userCredit.AvailableOn = userCreditModel.AvailableOn;
            userCredit.ExpiresOn = userCreditModel.ExpiresOn;
            _storeCreditService.InsertOrUpdate(userCredit);
            return R.Success.Result;
        }
    }
}