using System.Linq;
using EvenCart.Data.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;

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

        public static bool IsRequestForStaticResource()
        {
            if (ApplicationEngine.CurrentHttpContext.Request == null)
                return false;
            var provider = new FileExtensionContentTypeProvider();
            var path = ApplicationEngine.CurrentHttpContext.Request.Path.Value;

            var success = provider.TryGetContentType(path, out var contentType);
            return success;
        }
    }
}