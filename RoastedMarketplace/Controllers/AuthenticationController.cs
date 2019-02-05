using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Authentication;
using RoastedMarketplace.Services.Authentication;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Security;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Controllers
{
    public class AuthenticationController : FoundationController
    {
        private readonly IAppAuthenticationService _appAuthenticationService;
        private readonly UserSettings _userSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICryptographyService _cryptographyService;

        public AuthenticationController(IAppAuthenticationService appAuthenticationService, UserSettings userSettings, SecuritySettings securitySettings, IUserRegistrationService userRegistrationService, IRoleService roleService, IUserService userService, ICryptographyService cryptographyService)
        {
            _appAuthenticationService = appAuthenticationService;
            _userSettings = userSettings;
            _securitySettings = securitySettings;
            _userRegistrationService = userRegistrationService;
            _roleService = roleService;
            _userService = userService;
            _cryptographyService = cryptographyService;
        }

        [DualGet("login", Name = RouteNames.Login)]
        public IActionResult Login()
        {
            return Result();
        }

        [DualPost("login", Name = RouteNames.Login, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(LoginModel))]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ShouldSignIn(loginModel.Email, loginModel.Password, out User user))
                return R.Fail.With("message", T("Invalid email or password provided")).Result;

            var loginStatus = _appAuthenticationService.SignIn(loginModel.Email, user.Name, loginModel.RememberMe);
            if (loginStatus == LoginStatus.Success)
                return RedirectToRoute(RouteNames.Home);

            return R.Fail.With("message", T("An error occured while login")).Result;
        }

        [DualGet("register", Name = RouteNames.Register)]
        public IActionResult Register()
        {
            return R.Success.WithAvailableCountries().Result;
        }

        [DualPost("register", Name = RouteNames.Register, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(RegisterModel))]
        public IActionResult Register(RegisterModel registerModel)
        {
            var user = new User() {
                Email = registerModel.Email,
                Password = registerModel.Password,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                IsSystemAccount = false,
                Guid = Guid.NewGuid(),
                Active = _userSettings.UserRegistrationDefaultMode == RegistrationMode.Immediate
            };
            //register this user
            var registrationStatus = _userRegistrationService.Register(user, _securitySettings.DefaultPasswordStorageFormat);
            if (registrationStatus == UserRegistrationStatus.FailedAsEmailAlreadyExists)
                return R.Fail.With("message", "A user with this email is already registered").Result;

            var roleId = _roleService.Get(x => x.SystemName == SystemRoleNames.Registered).First().Id;
            //assign role to the user
            _roleService.SetUserRoles(user.Id, new[] {roleId});
            //raise the event
            RaiseEvent(NamedEvent.UserRegistered, user);

            return R.Success.Result;
        }

        #region Helpers

        [NonAction]
        private bool ShouldSignIn(string email, string password, out User user)
        {
            //get the user with the email
            user = _userService.FirstOrDefault(x => x.Email == email);
            if (user == null)
                return false;

            //get hashed password
            var hashedPassword = _cryptographyService.GetHashedPassword(password, user.PasswordSalt, user.PasswordFormat);
            return user.Password == hashedPassword;
        }
        #endregion
    }
}