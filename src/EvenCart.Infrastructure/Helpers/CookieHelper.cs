using System;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Helpers
{
    public static class CookieHelper
    {
        public static void SetResponseCookie(string cookieName, string cookieValue, bool isPersistent)
        {
            ApplicationEngine.CurrentHttpContext.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
            {
                Expires = isPersistent ? (DateTimeOffset?) DateTimeOffset.UtcNow.AddDays(365): null
            });
        }

        public static void SetResponseCookie(string cookieName, string cookieValue, int expirationHours)
        {
            ApplicationEngine.CurrentHttpContext.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddHours(expirationHours)
            });
        }

        public static string GetRequestCookie(string cookieName)
        {
            return ApplicationEngine.CurrentHttpContext.Request.Cookies[cookieName];
        }
    }
}