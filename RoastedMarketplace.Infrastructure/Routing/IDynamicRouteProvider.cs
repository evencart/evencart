using Microsoft.AspNetCore.Routing;
using RouteData = RoastedMarketplace.Core.Infrastructure.Routing.RouteData;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public interface IDynamicRouteProvider
    {
        VirtualPathData GetVirtualPathData(IRouter router, VirtualPathContext context);

        void RegisterDynamicRoute(RouteData routeData);

        RouteData GetMatchingRoute(IRouter router, RouteContext routeContext);
    }
}