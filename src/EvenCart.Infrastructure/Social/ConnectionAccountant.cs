using System;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Social;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Social;
using EvenCart.Services.Users;

namespace EvenCart.Infrastructure.Social
{
    public class ConnectionAccountant : IConnectionAccountant
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IConnectedAccountService _connectedAccountService;
        private readonly IUserService _userService;
        private readonly SecuritySettings _securitySettings;
        private readonly UserSettings _userSettings;
        private readonly IRoleService _roleService;
        public ConnectionAccountant(IUserRegistrationService userRegistrationService, IConnectedAccountService connectedAccountService, SecuritySettings securitySettings, IUserService userService, UserSettings userSettings, IRoleService roleService)
        {
            _userRegistrationService = userRegistrationService;
            _connectedAccountService = connectedAccountService;
            _securitySettings = securitySettings;
            _userService = userService;
            _userSettings = userSettings;
            _roleService = roleService;
        }

        public bool Connect(ConnectedAccountRequest request)
        {
            //check if the user is already connected
            var connectedAccount = _connectedAccountService.FirstOrDefault(x =>
                x.ProviderName == request.ProviderName && x.ProviderUserId == request.ProviderUserId);
            request.AutoLogin = true;
            //get user by email
            var user = _userService.GetByUserInfo(request.Email);
            if (connectedAccount == null)
            {
                if (request.Name.IsNullEmptyOrWhiteSpace())
                {
                    request.Name = $"{request.FirstName} {request.LastName}";
                }
                //connect the account
                //first register the user
                user = user ?? new User()
                {
                    Email = request.Email,
                    Password = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    IsSystemAccount = false,
                    Guid = Guid.NewGuid(),
                    Active = _userSettings.ActivateUserForConnectedAccount,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Name = request.Name
                };
                //register this user
                var status = _userRegistrationService.Register(user, _securitySettings.DefaultPasswordStorageFormat);
                if (status == UserRegistrationStatus.Success)
                {
                    //set the role
                    var roleId = _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Registered).Id;
                    //assign role to the user
                    _roleService.SetUserRoles(user.Id, new[] { roleId });
                }

                //it's possible that user is already registered with this email or it may be a new registration
                //in any case create a connected account
                connectedAccount = new ConnectedAccount()
                {
                    ProviderName = request.ProviderName,
                    ProviderUserId = request.ProviderUserId,
                    AccessToken = request.AccessToken,
                    UserId = user.Id
                };
                _connectedAccountService.Insert(connectedAccount);
            }
            else
            {
                //update token
                connectedAccount.AccessToken = request.AccessToken;
                _connectedAccountService.Update(connectedAccount);
            }
            //user is already connected, login if required
            if (request.AutoLogin)
            {
                //get the user
                var loginStatus = ApplicationEngine.SignIn(user.Email, user.Name, true);
                return loginStatus == LoginStatus.Success;
            }

            return true;
        }

        public bool IsConnected(string providerName, string providerUserId)
        {
            return _connectedAccountService.Count(x =>
                       x.ProviderName == providerName && x.ProviderUserId == providerUserId) > 0;
        }
    }
}