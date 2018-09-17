#region Author Information
// AuthenticationExtensions.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Authentication;

namespace RoastedMarketplace.Services.Extensions
{
    public static class AuthenticationExtensions
    {
        public static LoginStatus LoginAsVisitor(this IAppAuthenticationService authenticationService, string email, string name)
        {
            var loginStatus = authenticationService.SignIn(email, name, false, true);
            return loginStatus;
        }
    }
}