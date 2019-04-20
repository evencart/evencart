using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace EvenCart.Infrastructure
{
    public static class RequestHelper
    {
        public static bool IsApiCall()
        {
            return IsApiCall(out bool _, out string[] types);
        }

        public static bool IsApiCall(out bool withStoreMeta, out string[] types)
        {
            var httpContext = ApplicationEngine.CurrentHttpContext;
            var area = httpContext.GetRouteValue("area")?.ToString() ?? "";
            if (!area.IsNullEmptyOrWhiteSpace())
            {
                area = "/" + area;
            }
            var isApiCall = httpContext.Request.Path.Value.StartsWith(area + "/" + ApplicationConfig.ApiEndpointName + "/");
            withStoreMeta = isApiCall && httpContext.Request.Query["storeMeta"].Any();
            if (!withStoreMeta)
                types = null;
            types = httpContext.Request.Query["storeMeta"].ToArray();
            return isApiCall;
        }
    }
}