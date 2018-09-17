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
            var httpContextAccessor = DependencyResolver.Resolve<IHttpContextAccessor>();
            var actionContextAccessor = DependencyResolver.Resolve<IActionContextAccessor>();
            var area = "";
            if (actionContextAccessor.ActionContext.RouteData.Values.ContainsKey("area"))
            {
                area = actionContextAccessor.ActionContext.RouteData.Values["area"]?.ToString();
                if (!area.IsNullEmptyOrWhitespace())
                {
                    area = "/" + area;
                }
            }
            var isApiCall = httpContextAccessor.HttpContext.Request.Path.Value.StartsWith(area + "/" + ApplicationConfig.ApiEndpointName);
            return isApiCall;
        }
    }
}