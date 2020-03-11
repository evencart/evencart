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
using System.Security.Claims;
using System.Threading.Tasks;
using EvenCart.Core;
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

            if (!user.IsImitator(out _))
            {
                //update last activity date
                user.LastActivityDate = DateTime.UtcNow;
                user.LastActivityIpAddress = WebHelper.GetClientIpAddress();
                userService.Update(
                    new {LastActivityDate = DateTime.UtcNow, LastActivityIpAddress = WebHelper.GetClientIpAddress()},
                    x => x.Id == user.Id, null);
            }


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