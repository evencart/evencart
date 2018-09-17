using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Security;
using RoastedMarketplace.Services.Users;
using IAppAuthenticationService = RoastedMarketplace.Services.Authentication.IAppAuthenticationService;

namespace RoastedMarketplace.Infrastructure.Authentication
{
    public class AuthenticationService : IAppAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly SecuritySettings _securitySettings;
        private readonly ICryptographyService _cryptographyService;
        private User _user;

        public AuthenticationService(IUserService userService,
            IUserRegistrationService userRegistrationService,
            SecuritySettings securitySettings,
            ICryptographyService cryptographyService)
        {
            _userService = userService;
            _userRegistrationService = userRegistrationService;
            _securitySettings = securitySettings;
            _cryptographyService = cryptographyService;
        }

        public virtual LoginStatus SignIn(string email, string name = "", bool isPersistent = false, bool forceCreateNewAccount = false)
        {
            var user = _userService.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                //do we need to force create a new user
                if (forceCreateNewAccount)
                {
                    var password = _cryptographyService.GetRandomPassword();
                    //register now
                    var registrationStatus = _userRegistrationService.Register(email, password, _securitySettings.DefaultPasswordStorageFormat);

                    if (registrationStatus == UserRegistrationStatus.FailedAsEmailAlreadyExists)
                        return LoginStatus.Failed;

                    //load user again
                    if (registrationStatus == UserRegistrationStatus.Success)
                    {
                        user = _userService.FirstOrDefault(x => x.Email == email);
                        if (!string.IsNullOrEmpty(name))
                        {
                            user.Name = name;
                            _userService.Update(user);
                        }
                    }
                }
            }
            if (user == null)
                return LoginStatus.FailedUserNotExists;

            if (user.Deleted)
                return LoginStatus.FailedDeletedUser;

            if (user.IsSystemAccount)
                return LoginStatus.Failed;

            if (!user.Active)
                return LoginStatus.FailedInactiveUser;

            //create the authentication ticket for the user
            CreateAuthenticationTicket(user, isPersistent);

            return LoginStatus.Success;
        }

        public virtual void SignOut()
        {
            ClearAuthenticationTicket();
        }

        public virtual async void CreateAuthenticationTicket(User user, bool isPersistent = false)
        {
            //sign in the user. this will create the authentication cookie
            await ApplicationEngine.CurrentHttpContext.SignInAsync(ApplicationConfig.DefaultAuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                })), new AuthenticationProperties() {
                    IsPersistent = true
                });

            _user = user;
        }

        public virtual async void ClearAuthenticationTicket()
        {
            await ApplicationEngine.CurrentHttpContext.SignOutAsync(ApplicationConfig.DefaultAuthenticationScheme);
        }

        public virtual User GetCurrentUser()
        {
            if (_user != null)
                return _user;

            var authencationResult = ApplicationEngine.CurrentHttpContext.AuthenticateAsync(ApplicationConfig.DefaultAuthenticationScheme).Result;
            if (!authencationResult.Succeeded)
                return null;

            var claimsPrincipal = authencationResult.Principal;
            var emailClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.Email);
            var guidClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            if (emailClaim == null || guidClaim == null)
                return null;

            var email = emailClaim.Value;
            var guid = guidClaim.Value;

            _user = _userService.GetByUserInfo(email, guid);
            if (_user == null)
                return null;
            //user must be active and not deleted
            if (!_user.Active || _user.Deleted)
                _user = null;

            return _user;
        }
    }
}