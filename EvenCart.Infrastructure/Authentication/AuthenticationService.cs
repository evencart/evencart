using System.Security.Claims;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Data.Helpers;
using EvenCart.Services.Security;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;

namespace EvenCart.Infrastructure.Authentication
{
    public class AuthenticationService : Services.Authentication.IAppAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly SecuritySettings _securitySettings;
        private readonly ICryptographyService _cryptographyService;

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
            return SignIn(ApplicationConfig.DefaultAuthenticationScheme, email, name, isPersistent,
                forceCreateNewAccount);
        }

        public LoginStatus SignIn(string authenticationScheme, string email, string name = "", bool isPersistent = false,
            bool forceCreateNewAccount = false)
        {
            return SignInImpl(authenticationScheme, email, name, isPersistent, forceCreateNewAccount);
        }

        public LoginStatus ImitationModeSignIn(string targetUserEmail, string imitatorEmail)
        {
            return SignInImpl(ApplicationConfig.DefaultAuthenticationScheme, targetUserEmail, "", false, false,
                imitatorEmail);
        }

        public virtual void SignOut()
        {
            var user = GetCurrentUser();
            ClearAuthenticationTicket();

            //is imitator?
            if (user.IsImitator(out var imitatorEmail))
            {
                //signin the imitator
                var isPersistantLogin = user.GetMeta<bool>(ApplicationConfig.PersistanceKey);
                SignIn(imitatorEmail, "", isPersistantLogin);
            }
        }

        public LoginStatus GuestSignIn()
        {
            var guestEmail = HtmlUtility.GetRandomEmail();
            var status = SignIn(ApplicationConfig.VisitorAuthenticationScheme, guestEmail, null, true, true);
            return status;
        }

        public virtual async void CreateAuthenticationTicket(User user, bool isPersistent = false, string authenticationScheme = ApplicationConfig.DefaultAuthenticationScheme, string imitatorEmail = null)
        {
            //sign in the user. this will create the authentication cookie
            await ApplicationEngine.CurrentHttpContext.SignInAsync(authenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                    new Claim(ClaimTypes.Actor, imitatorEmail ?? ""),
                    new Claim(ClaimTypes.IsPersistent, isPersistent.ToString()), 
                }, authenticationScheme)), new AuthenticationProperties()
                {
                    IsPersistent = isPersistent
                });
        }

        public virtual async void ClearAuthenticationTicket()
        {
            await ApplicationEngine.CurrentHttpContext.SignOutAsync(ApplicationConfig.DefaultAuthenticationScheme);
            await ApplicationEngine.CurrentHttpContext.SignOutAsync(ApplicationConfig.VisitorAuthenticationScheme);
        }

        public virtual User GetCurrentUser()
        {
            var hc = ApplicationEngine.CurrentHttpContext;

            var user = hc.GetCurrentUser();
            if (user != null)
                return user;

            var authenticationResult = hc.AuthenticateAsync(ApplicationConfig.DefaultAuthenticationScheme).Result;
            if (!authenticationResult.Succeeded)
            {
                //is there a visitor there?
                hc.AuthenticateAsync(ApplicationConfig.VisitorAuthenticationScheme);
            }

            return hc.GetCurrentUser();
        }

        #region Helpers
        private LoginStatus SignInImpl(string authenticationScheme, string email, string name = "", bool isPersistent = false,
            bool forceCreateNewAccount = false, string imitatorEmail = null)
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
            CreateAuthenticationTicket(user, isPersistent, authenticationScheme, imitatorEmail);

            if (!imitatorEmail.IsNullEmptyOrWhiteSpace())
                user.SetMeta(ApplicationConfig.ImitatorKey, imitatorEmail);
            //persist user
            ApplicationEngine.CurrentHttpContext.SetCurrentUser(user);

            return LoginStatus.Success;
        }
        #endregion
    }
}