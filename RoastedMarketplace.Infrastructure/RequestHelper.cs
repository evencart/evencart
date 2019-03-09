using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoastedMarketplace.Core.Extensions;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure
{
    public static class RequestHelper
    {
        public static bool IsApiCall()
        {
            return IsApiCall(out bool _, out string[] types);
        }

        public static bool IsApiCall(out bool withStoreMeta, out string[] types)
        {
            var actionContextAccessor = DependencyResolver.Resolve<IActionContextAccessor>();
            var actionContext = actionContextAccessor.ActionContext;
            var httpContext = ApplicationEngine.CurrentHttpContext;
            var area = "";
            if (actionContext?.RouteData.Values.ContainsKey("area") ?? false)
            {
                area = actionContext.RouteData.Values["area"]?.ToString();
                if (!area.IsNullEmptyOrWhitespace())
                {
                    area = "/" + area;
                }
            }
            var isApiCall = httpContext.Request.Path.Value.StartsWith(area + "/" + ApplicationConfig.ApiEndpointName);
            withStoreMeta = isApiCall && httpContext.Request.Query["storeMeta"].Any();
            if (!withStoreMeta)
                types = null;
            types = httpContext.Request.Query["storeMeta"].ToArray();
            return isApiCall;
        }
    }
}