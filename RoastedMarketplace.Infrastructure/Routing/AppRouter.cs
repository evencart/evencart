using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class AppRouter : IRouter
    {
        private readonly IRouter _defaultRouter;
        private readonly IDynamicRouteProvider _dynamicRouteProvider;
        public AppRouter(IRouter defaultRouter)
        {
            _defaultRouter = defaultRouter;
            _dynamicRouteProvider = DependencyResolver.Resolve<IDynamicRouteProvider>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            _dynamicRouteProvider.GetMatchingRoute(this, context);
            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return _dynamicRouteProvider.GetVirtualPathData(this, context);
        }
    }
}