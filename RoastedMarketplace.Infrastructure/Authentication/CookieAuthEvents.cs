using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Services.Extensions;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Infrastructure.Authentication
{
    public class CookieAuthEvents : CookieAuthenticationEvents
    {
        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userService = DependencyResolver.Resolve<IUserService>();
            //check if user has been deleted/deactivated
            var claimsPrincipal = context.Principal;
            var emailClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.Email);
            var guidClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            if (emailClaim == null || guidClaim == null)
            {
                context.RejectPrincipal();
                return Task.CompletedTask;
            }

            var email = emailClaim.Value;
            var guid = guidClaim.Value;

            var user = userService.GetByUserInfo(email, guid);

            //user must be active and not deleted
            if (user == null || !user.Active || user.Deleted)
            {
                context.RejectPrincipal();
                return Task.CompletedTask;
            }
            //preserve user
            ApplicationEngine.CurrentHttpContext.SetCurrentUser(user);
            //but reject for a visitor. This way we allow anonymous activity like adding products and guest checkout 
            //while still rejecting the authorization so the secure area can't be accessed without login
            if (user.IsVisitor())
            {
                context.RejectPrincipal();
                return Task.CompletedTask;
            }
            return base.ValidatePrincipal(context);
        }
    }
}