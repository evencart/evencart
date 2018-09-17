using Microsoft.AspNetCore.Routing;

namespace RoastedMarketplace.Core.Infrastructure.Routing
{
    public interface IRouteMap
    {
        void MapRoutes(RouteCollection routes);
    }
}