using System;
using System.Threading.Tasks;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Services.Extensions;
using EvenCart.Services.Users;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class AffiliateTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public AffiliateTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //do we have a cookie 
            if (DatabaseManager.IsDatabaseInstalled())
            {
                var affiliateSettings = DependencyResolver.Resolve<AffiliateSettings>();
                var userService = DependencyResolver.Resolve<IUserService>();
                if (affiliateSettings.EnableAffiliates)
                {
                    //do we have any affiliate uid
                    var affiliateGuid = CookieHelper.GetRequestCookie(affiliateSettings.AffiliateCookieName);
                    var currentUser = ApplicationEngine.CurrentUser;
                    if (!affiliateGuid.IsNullEmptyOrWhiteSpace() && Guid.TryParse(affiliateGuid, out var aGuid))
                    {
                        var affiliateUser = userService.FirstOrDefault(x => x.Guid == aGuid);
                        if (affiliateUser.IsActiveAffiliate() && (currentUser == null || currentUser.Guid != aGuid))
                        {
                            context.SetCurrentAffiliate(affiliateUser);
                        }
                    }
                    else
                    {
                        //check if there is a query string parameter
                        if (context.Request.Query.ContainsKey(ApplicationConfig.AffiliateIdQueryStringParameterName))
                        {
                            affiliateGuid = context.Request.Query[ApplicationConfig.AffiliateIdQueryStringParameterName];
                            if (!affiliateGuid.IsNullEmptyOrWhiteSpace() && Guid.TryParse(affiliateGuid, out aGuid))
                            {
                                var affiliateUser = userService.FirstOrDefault(x => x.Guid == aGuid);
                                if (affiliateUser.IsActiveAffiliate() && (currentUser == null || currentUser.Guid != aGuid))
                                {
                                    context.SetCurrentAffiliate(affiliateUser);
                                    //and set a cookie as well
                                    CookieHelper.SetResponseCookie(affiliateSettings.AffiliateCookieName, affiliateGuid,
                                        affiliateSettings.AffiliateCookieExpirationDays * 24); //this will automatically expire the cookie after certain period
                                }
                            }
                        }
                    }
                }
            }
            await _next.Invoke(context);
        }
    }
}