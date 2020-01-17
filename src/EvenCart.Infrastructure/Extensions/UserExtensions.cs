using System;
using System.Web;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static bool IsImitator(this User user, out string imitator)
        {
            imitator = user.GetMeta<string>(ApplicationConfig.ImitatorKey);
            return !imitator.IsNullEmptyOrWhiteSpace();
        }

        public static string GetAffiliateUrl(this User user, string baseUrl = null)
        {
            baseUrl = baseUrl ?? ApplicationEngine.RouteUrl(RouteNames.Home, absoluteUrl: true);
            var uri = new UriBuilder(baseUrl);
            var queryParameters = HttpUtility.ParseQueryString(uri.Query);
            queryParameters[ApplicationConfig.AffiliateIdQueryStringParameterName] = user.Guid.ToString();
            uri.Query = queryParameters.ToString();
            return uri.ToString();
        }
    }
}