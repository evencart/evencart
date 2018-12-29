using Microsoft.AspNetCore.Routing;
using RoastedMarketplace.Data.Entity.Page;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public interface IDynamicRouteProvider
    {
        DynamicRoute GetDynamicRoute(SeoMeta seoMeta);

        VirtualPathData GetVirtualPathData(IRouter router, VirtualPathContext context);
    }
}