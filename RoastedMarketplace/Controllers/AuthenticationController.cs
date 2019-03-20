using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure;
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
        private readonly IUserCodeService _userCodeService;
        private readonly IPreviousPasswordService _previousPasswordService;
        public AuthenticationController(IAppAuthenticationService appAuthenticationService, UserSettings userSettings, SecuritySettings securitySettings, IUserRegistrationService userRegistrationService, IRoleService roleService, IUserService userService, ICryptographyService cryptographyService, IUserCodeService userCodeService, IPreviousPasswordService previousPasswordService)
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
        }

        [HttpGet("login", Name = RouteNames.Login)]
        public IActionResult Login()
        {
            return R.Success.Result;
        }

        [DualPost("login", Name = RouteNames.Login, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(LoginModel))]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ShouldSignIn(loginModel.Email, loginModel.Password, out User user))
                return R.Fail.With("message", T("Invalid email or password provided")).Result;

            var loginStatus = ApplicationEngine.SignIn(loginModel.Email, user.Name, loginModel.RememberMe);
            if (loginStatus == LoginStatus.Success)
                return R.Success.Result;

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


        [DualGet("register", Name = RouteNames.Register)]
        public IActionResult Register()
        {
            return R.Success.WithAvailableCountries().Result;
        }

        [DualGet("logout", Name = RouteNames.Logout)]
        public IActionResult Logout()
        {
            _appAuthenticationService.SignOut();
            return Redirect(Url.RouteUrl(RouteNames.Home));
        }


        [DualPost("register", Name = RouteNames.Register, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(RegisterModel))]
        public IActionResult Register(RegisterModel registerModel)
        {
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