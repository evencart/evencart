using System.Security.Claims;
using System.Threading.Tasks;
using EvenCart.Core.Extensions;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;
using EvenCart.Services.Extensions;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EvenCart.Infrastructure.Authentication
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

            var actorClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.Actor);
            if (actorClaim != null && !actorClaim.Value.IsNullEmptyOrWhiteSpace())
                user.SetMeta(ApplicationConfig.ImitatorKey, actorClaim.Value);
            var persistanceClaim = claimsPrincipal.FindFirst(x => x.Type == ClaimTypes.IsPersistent);
            var isPersistant = persistanceClaim?.Value == (true).ToString();
            user.SetMeta(ApplicationConfig.PersistanceKey, isPersistant);
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