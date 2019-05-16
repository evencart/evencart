using System;
using System.Linq;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Services.Authentication;
using EvenCart.Services.Extensions;
using EvenCart.Services.Gdpr;
using EvenCart.Services.Security;
using EvenCart.Services.Users;
using EvenCart.Events;
using EvenCart.Factories.Gdpr;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Authentication;
using EvenCart.Models.Gdpr;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Provides authentication related services including registration, login, password recovery etc.
    /// </summary>
    public class AuthenticationController : FoundationController
    {
        private readonly IAppAuthenticationService _appAuthenticationService;
        private readonly UserSettings _userSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IUserCodeService _userCodeService;
        private readonly IPreviousPasswordService _previousPasswordService;
        private readonly IConsentService _consentService;
        private readonly IGdprService _gdprService;
        private readonly IGdprModelFactory _gdprModelFactory;

        public AuthenticationController(IAppAuthenticationService appAuthenticationService, UserSettings userSettings, SecuritySettings securitySettings, IUserRegistrationService userRegistrationService, IRoleService roleService, IUserService userService, ICryptographyService cryptographyService, IUserCodeService userCodeService, IPreviousPasswordService previousPasswordService, IConsentService consentService, IGdprService gdprService, IGdprModelFactory gdprModelFactory)
        {
            _appAuthenticationService = appAuthenticationService;
            _userSettings = userSettings;
            _securitySettings = securitySettings;
            _userRegistrationService = userRegistrationService;
            _roleService = roleService;
            _userService = userService;
            _cryptographyService = cryptographyService;
            _userCodeService = userCodeService;
            _previousPasswordService = previousPasswordService;
            _consentService = consentService;
            _gdprService = gdprService;
            _gdprModelFactory = gdprModelFactory;
        }

        [HttpGet("login", Name = RouteNames.Login)]
        public IActionResult Login()
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor())
                return RedirectToRoute(RouteNames.Home);
            return R.Success.Result;
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <response code="200">An authentication cookie if token was set to false. Returns a token string if token parameter was sent as true.</response>
        [DualPost("login", Name = RouteNames.Login, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(LoginModel))]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ShouldSignIn(loginModel.Email, loginModel.Password, out User user))
                return R.Fail.With("message", T("Invalid email or password provided")).Result;

            var loginStatus = ApplicationEngine.SignIn(loginModel.Email, user.Name, loginModel.RememberMe, loginModel.Token);
            //get api token if it was requested
            var token = ApplicationEngine.CurrentHttpContext.GetApiToken();
            if (loginStatus == LoginStatus.Success)
                return R.Success.With("token", token).Result;

            return R.Fail.With("message", T("An error occured while login")).Result;
        }


        [HttpGet("password-reset", Name = RouteNames.ForgotPassword)]
        public IActionResult ForgotPassword(string code = null)
        {
            var response = R;
            if (code != null || ApplicationEngine.CurrentUser != null)
            {
                UserCode userCode = null;
                if (ApplicationEngine.CurrentUser.IsVisitor())
                {
                    //verify if it's a valid code
                    userCode = _userCodeService.GetUserCode(code, UserCodeType.PasswordReset);
                    if (!IsCodeValid(userCode))
                    {
                        return response.Fail.With("expired", true).Result;
                    }
                }

                var userId = userCode?.UserId ?? ApplicationEngine.CurrentUser.Id;
                response.Success.With("validCode", true);
                //regenerate the code so the same can't be used again
                userCode = _userCodeService.GetUserCode(userId, UserCodeType.PasswordReset);
                response.Success.With("code", userCode.Code);
            }
            return response.Result;
        }

        /// <summary>
        /// Sends a password recovery email to the user
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <response code="200">a success response object and sends password reset email to the user</response>
        [DualPost("password-reset", Name = RouteNames.ForgotPassword, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(ForgotPasswordModel))]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            //check if the user exist
            var user = _userService.GetByUserInfo(forgotPasswordModel.Email);
            if (user == null)
                return R.Fail.With("error", T("There is no account associated with this email address")).Result;

            //generate user code
            var userCode = _userCodeService.GetUserCode(user.Id, UserCodeType.PasswordReset);
            var passwordResetUrl = ApplicationEngine.RouteUrl(RouteNames.ForgotPassword, new { code = userCode.Code }, true);
            RaiseEvent(NamedEvent.PasswordResetRequested, user, passwordResetUrl);
            return R.Success.Result;
        }

        /// <summary>
        /// Resets the password of the user
        /// </summary>
        /// <param name="changeModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("password-change", Name = RouteNames.ChangePassword, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(PasswordChangeModel))]
        public IActionResult ChangePassword(PasswordChangeModel changeModel)
        {
            var userCode = _userCodeService.GetUserCode(changeModel.Code, UserCodeType.PasswordReset);
            if (!IsCodeValid(userCode))
            {
                return R.Fail.With("expired", true).Result;
            }

            //check if current password needs to be checked
            if (ApplicationEngine.CurrentUser.IsRegistered())
            {
                //we do
                if (!ShouldSignIn(ApplicationEngine.CurrentUser, changeModel.CurrentPassword))
                {
                    return R.Fail.With("error", T("The current password is invalid")).Result;
                }
            }

            //update the password
            //first preserve the old password
            _previousPasswordService.Insert(new PreviousPassword()
            {
                UserId = userCode.UserId,
                Password = userCode.User.Password,
                PasswordSalt = userCode.User.PasswordSalt,
                PasswordFormat = userCode.User.PasswordFormat,
                DateCreated = DateTime.UtcNow
            });

            //reset the password now
            _userRegistrationService.UpdatePassword(userCode.UserId, changeModel.Password,
                _securitySettings.DefaultPasswordStorageFormat);

            //delete the user code now
            _userCodeService.Delete(x => x.UserId == userCode.UserId && x.CodeType == UserCodeType.PasswordReset);

            RaiseEvent(NamedEvent.PasswordReset, userCode.User);
            return R.Success.Result;
        }

        /// <summary>
        /// Gets the consents(if any) required to complete the registration
        /// </summary>
        /// <response>A list of <see cref="ConsentModel">consents</see></response>
        [DualGet("register", Name = RouteNames.Register)]
        public IActionResult Register()
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor())
                return RedirectToRoute(RouteNames.Home);
            //are registrations enabled?
            if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.Disabled)
                return R.Fail.With("registrationDisabled", true).Result;

            //get one time consents
            var consents = _consentService.Get(x => x.OneTimeSelection && x.Published).ToList();
            var models = consents.Select(_gdprModelFactory.Create).ToList();
            return R.Success.With("consents", models).WithAvailableCountries().Result;
        }
        /// <summary>
        /// Logs the current user out. Valid only for cookie authentication
        /// </summary>
        /// <returns></returns>
        [DualGet("logout", Name = RouteNames.Logout)]
        public IActionResult Logout()
        {
            //gather some data before signing out
            var currentUserId = CurrentUser.Id;
            var imitation = CurrentUser.IsImitator(out _);
            //sign out active session now
            _appAuthenticationService.SignOut();

            //do we have an imitation active/
            if (imitation)
            {
                return RedirectToRoute(AdminRouteNames.UserImitate, new { userId = currentUserId });
            }
            return Redirect(Url.RouteUrl(RouteNames.Home));
        }

        /// <summary>
        /// Registers a new user on the site
        /// </summary>
        /// <param name="registerModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("register", Name = RouteNames.Register, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(RegisterModel))]
        public IActionResult Register(RegisterModel registerModel)
        {
            //are registrations enabled?
            if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.Disabled)
                return R.Fail.With("error", T("New registrations are disabled at the moment")).Result;
            //validate consents first
            //get one time consents
            var consents = _consentService.Get(x => x.OneTimeSelection && x.Published).ToList();
            if (consents.Any(x => x.IsRequired))
            {
                foreach (var requiredConsent in consents.Where(x => x.IsRequired))
                {
                    var sentModel = registerModel.Consents.FirstOrDefault(x => x.Id == requiredConsent.Id);
                    if (sentModel == null || sentModel.ConsentStatus != ConsentStatus.Accepted)
                        return R.Fail.With("error", T("Please consent to '" + requiredConsent.Title + "'")).Result;
                }
            }
            var user = new User()
            {
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
            _roleService.SetUserRoles(user.Id, new[] { roleId });

            //save the consents
            var consentDictionary = registerModel.Consents.ToDictionary(x => x.Id, x => x.ConsentStatus);
            _gdprService.SetUserConsents(user.Id, consentDictionary);

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
            return ShouldSignIn(user, password);
        }
        [NonAction]
        private bool ShouldSignIn(User user, string password)
        {
            //get hashed password
            var hashedPassword = _cryptographyService.GetHashedPassword(password, user.PasswordSalt, user.PasswordFormat);
            return user.Password == hashedPassword;
        }

        private bool IsCodeValid(UserCode userCode)
        {
            if (userCode == null)
            {
                return false;
            }
            return _securitySettings.PasswordResetLinkExpirationHours <= 0 || DateTime.UtcNow.Subtract(userCode.DateCreated).Hours <= _securitySettings.PasswordResetLinkExpirationHours;
        }
        #endregion
    }
}