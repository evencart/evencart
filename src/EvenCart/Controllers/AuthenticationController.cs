#region License
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
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
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
using EvenCart.Infrastructure.Plugins;
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
        private readonly IInviteRequestService _inviteRequestService;
        private readonly AffiliateSettings _affiliateSettings;
        public AuthenticationController(IAppAuthenticationService appAuthenticationService, UserSettings userSettings, SecuritySettings securitySettings, IUserRegistrationService userRegistrationService, IRoleService roleService, IUserService userService, ICryptographyService cryptographyService, IUserCodeService userCodeService, IPreviousPasswordService previousPasswordService, IConsentService consentService, IGdprService gdprService, IGdprModelFactory gdprModelFactory, IInviteRequestService inviteRequestService, AffiliateSettings affiliateSettings)
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
            _inviteRequestService = inviteRequestService;
            _affiliateSettings = affiliateSettings;
        }

        [HttpGet("login", Name = RouteNames.Login)]
        public IActionResult Login()
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor())
                return RedirectToRoute(RouteNames.Home);
            //are there any active login methods available 
            var otherLoginMethodsAvailable =
                DependencyResolver.Resolve<IPluginAccountant>().GetActiveWidgetCount(widgetZone: "login") > 0;
            return R.Success.With("otherLoginsAvailable", otherLoginMethodsAvailable).Result;
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
                CreatedOn = DateTime.UtcNow
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
        /// <param name="requestModel"></param>
        /// <response>A list of <see cref="ConsentModel">consents</see></response>
        [DualGet("register", Name = RouteNames.Register)]
        public IActionResult Register(RegisterRequestModel requestModel)
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor())
                return RedirectToRoute(RouteNames.Home);
            //are registrations enabled?
            if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.Disabled)
                return R.Fail.With("registrationDisabled", true).Result;
            var inviteCode = requestModel?.InviteCode;
            if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.InviteOnly)
            {
                if (inviteCode.IsNullEmptyOrWhiteSpace())
                    return R.Fail.With("registrationDisabled", true).With("allowInvites", true).Result;
                var userCode = _userCodeService.GetUserCode(inviteCode, UserCodeType.RegistrationInvitation);
                if(!IsCodeValid(userCode))
                    return R.Fail.With("registrationDisabled", true).With("allowInvites", true).Result;

            }

            //get one time consents
            var consents = _consentService.Get(x => x.OneTimeSelection && x.Published).ToList();
            var models = consents.Select(_gdprModelFactory.Create).ToList();
            return R.Success
                .With("consents", models)
                .With("inviteCode", inviteCode)
                .WithAvailableCountries()
                .With("mode", _userSettings.UserRegistrationDefaultMode)
                .With("numericActivation", _userSettings.UseNumericCodeForActivationEmail).Result;
        }

        /// <summary>
        /// Logs the current user out. Valid only for cookie authentication
        /// </summary>
        /// <response code="200">A success response object</response>
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
            var inviteCode = registerModel.InviteCode;
            UserCode userCode = null;
            if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.InviteOnly)
            {
                if (inviteCode.IsNullEmptyOrWhiteSpace())
                    return R.Fail.With("error", T("Registrations are allowed only by invitation")).Result;
                userCode = _userCodeService.GetUserCode(inviteCode, UserCodeType.RegistrationInvitation);
                if (userCode.Email != registerModel.Email || !IsCodeValid(userCode))
                    return R.Fail.With("error", T("Registrations are allowed only by invitation")).Result;

            }
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
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                IsSystemAccount = false,
                Guid = Guid.NewGuid(),
                Active = _userSettings.UserRegistrationDefaultMode == RegistrationMode.Immediate || _userSettings.UserRegistrationDefaultMode == RegistrationMode.InviteOnly
            };
            if (user.Active)
            {
                user.FirstActivationDate = DateTime.UtcNow;
            }
            //register this user
            var registrationStatus = _userRegistrationService.Register(user, _securitySettings.DefaultPasswordStorageFormat);
            if (registrationStatus == UserRegistrationStatus.FailedAsEmailAlreadyExists)
                return R.Fail.With("message", "A user with this email is already registered").Result;

            var roleId = _roleService.Get(x => x.SystemName == SystemRoleNames.Registered).First().Id;
            //assign role to the user
            _roleService.SetUserRoles(user.Id, new[] { roleId });

            if (registerModel.Consents != null && registerModel.Consents.Any())
            {
                //save the consents
                var consentDictionary = registerModel.Consents.ToDictionary(x => x.Id, x => x.ConsentStatus);
                _gdprService.SetUserConsents(user.Id, consentDictionary);
            }

            //delete the invite code & user code if any
            _inviteRequestService.Delete(x => x.Email == registerModel.Email);
            if (userCode != null)
                _userCodeService.Delete(userCode);
            var verificationLink = "";
            if (registerModel.InviteCode.IsNullEmptyOrWhiteSpace())
            {
                //if there was no invite code, the email needs to be verified (if the admin wants so)
                if (_userSettings.UserRegistrationDefaultMode == RegistrationMode.WithActivationEmail)
                {
                    userCode = _userCodeService.GetUserCode(user.Id, _userSettings.UseNumericCodeForActivationEmail ? UserCodeType.EmailOtp : UserCodeType.EmailVerification);
                    var verificationCode = userCode.Code;
                    verificationLink = verificationCode;
                    if (!_userSettings.UseNumericCodeForActivationEmail)
                    {
                        verificationLink =
                            ApplicationEngine.RouteUrl(RouteNames.VerifyEmail, new { code = verificationCode }, true);
                    }
                }
            }

            //do we have any affiliate?
            var affiliate = ApplicationEngine.CurrentAffiliate;
            if (affiliate != null)
            {
                user.ReferrerId = affiliate.Id;
                _userService.Update(user);
            }

            //raise the event
            RaiseEvent(NamedEvent.UserRegistered, user, verificationLink);
            if (user.Active)
            {
                RaiseEvent(NamedEvent.UserActivated, user);
            }
            return R.Success.With("mode", _userSettings.UserRegistrationDefaultMode).With("numericActivation", _userSettings.UseNumericCodeForActivationEmail).Result;
        }

        [HttpGet("request-invite", Name = RouteNames.RequestInvite)]
        public IActionResult RequestInvite()
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor() || _userSettings.UserRegistrationDefaultMode != RegistrationMode.InviteOnly)
                return RedirectToRoute(RouteNames.Home);
            return R.Success.Result;
        }

        /// <summary>
        /// Adds an invitation request to join the site
        /// </summary>
        /// <param name="requestModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("request-invite", Name = RouteNames.RequestInvite, OnlyApi = true)]
        public IActionResult RequestInvite(InviteRequestModel requestModel)
        {
            //if already logged in, redirect to home
            if (!CurrentUser.IsVisitor())
                return R.Fail.Result;
            //check if the email being requested is already a regsitered user
            var user = _userService.GetByUserInfo(requestModel.Email);
            if (user != null)
            {
                return R.Fail.With("error", T("A user with this email is already registered")).Result;
            }
            //do we already have a request with this email
            var inviteRequest = _inviteRequestService.FirstOrDefault(x => x.Email == requestModel.Email);
            if (inviteRequest == null)
            {
                inviteRequest = new InviteRequest()
                {
                    Email = requestModel.Email,
                    CreatedOn = DateTime.UtcNow
                };
                _inviteRequestService.Insert(inviteRequest);
                RaiseEvent(NamedEvent.InvitationRequested, requestModel.Email);
            }
            return R.Success.Result;
        }

        /// <summary>
        /// Verifies the email address
        /// </summary>
        /// <param name="code">The verification code</param>
        /// <returns></returns>
        [HttpGet("verify-email", Name = RouteNames.VerifyEmail)]
        public IActionResult VerifyEmail(string code)
        {
            if (code.IsNullEmptyOrWhiteSpace())
                return RedirectToRoute(RouteNames.Home);
            var userCode = _userCodeService.GetUserCode(code, UserCodeType.EmailVerification);
            if (!IsCodeValid(userCode))
                return RedirectToRoute(RouteNames.Home);
            //activate the user
            userCode.User.Active = true;
            _userService.Update(userCode.User);
            //delete the user code
            _userCodeService.Delete(userCode);
            RaiseEvent(NamedEvent.UserActivated, userCode.User);
            return R.Success.Result;
        }

        /// <summary>
        /// Verifies the email address with the code and immediately logs in the user
        /// </summary>
        /// <param name="verificationModel"></param>
        /// <response code="200">A success response object</response>
        [DualPost("verify-email", Name = RouteNames.VerifyEmail, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(EmailVerificationModel))]
        public IActionResult VerifyEmail(EmailVerificationModel verificationModel)
        {
            var userCode = _userCodeService.GetUserCode(verificationModel.Code, UserCodeType.EmailOtp);
            if (userCode == null || userCode.User.Email != verificationModel.Email || !IsCodeValid(userCode) || verificationModel.Code != userCode.Code)
            {
                return R.Fail.With("error", T("The code is invalid or expired")).Result;
            }

            userCode.User.Active = true;
            _userService.Update(userCode.User);
            //delete the user code
            _userCodeService.Delete(userCode);
            RaiseEvent(NamedEvent.UserActivated, userCode.User);
            //and signin the current user
            var loginStatus = ApplicationEngine.SignIn(userCode.User.Email, userCode.User.Name, false, verificationModel.Token);
            //get api token if it was requested
            var token = ApplicationEngine.CurrentHttpContext.GetApiToken();
            if (loginStatus == LoginStatus.Success)
                return R.Success.With("token", token).Result;

            return R.Fail.With("message", T("An error occured while login")).Result;
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
            if (user.PasswordFormat == PasswordFormat.Plain)
                return user.Password == password;
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

            if (userCode.CodeType == UserCodeType.PasswordReset)
                return _securitySettings.PasswordResetLinkExpirationHours <= 0 ||
                       DateTime.UtcNow.Subtract(userCode.CreatedOn).Hours <=
                       _securitySettings.PasswordResetLinkExpirationHours;

            if (userCode.CodeType == UserCodeType.RegistrationInvitation)
                return _securitySettings.InviteLinkExpirationHours <= 0 ||
                       DateTime.UtcNow.Subtract(userCode.CreatedOn).Hours <=
                       _securitySettings.InviteLinkExpirationHours;

            if (userCode.CodeType == UserCodeType.EmailVerification)
                return _securitySettings.EmailVerificationLinkExpirationHours <= 0 ||
                       DateTime.UtcNow.Subtract(userCode.CreatedOn).Hours <=
                       _securitySettings.EmailVerificationLinkExpirationHours;

            if (userCode.CodeType == UserCodeType.EmailOtp)
                return _securitySettings.EmailVerificationCodeExpirationMinutes <= 0 ||
                       DateTime.UtcNow.Subtract(userCode.CreatedOn).Minutes <=
                       _securitySettings.EmailVerificationCodeExpirationMinutes;

            return false;
        }
        #endregion
    }
}