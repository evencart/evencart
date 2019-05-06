using Microsoft.AspNetCore.Routing;
using RouteData = EvenCart.Core.Infrastructure.Routing.RouteData;

namespace EvenCart.Infrastructure.Routing
{
    public interface IDynamicRouteProvider
    {
        VirtualPathData GetVirtualPathData(IRouter router, VirtualPathContext context);

        void RegisterDynamicRoute(RouteData routeData);

        RouteData GetMatchingRoute(IRouter router, RouteContext routeContext);
    }
}