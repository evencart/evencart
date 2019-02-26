using Microsoft.AspNetCore.Http;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure;

namespace RoastedMarketplace
{
    public static class HttpContextExtensions
    {
        private const string CurrentUserKey = "CurrentUser";

        public static void SetCurrentUser(this HttpContext httpContext, User user)
        {
            httpContext.Items[CurrentUserKey] = user;
        }

        public static User GetCurrentUser(this HttpContext httpContext)
        {
            return (User)httpContext.Items[CurrentUserKey];
        }
    }
}