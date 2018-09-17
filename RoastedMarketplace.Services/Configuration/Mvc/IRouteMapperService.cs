using System.Web.Routing;

namespace RoastedMarketplace.Services.Configuration.Mvc
{
    public interface IRouteMapperService
    {
        /// <summary>
        /// Maps all the routes across application
        /// </summary>
        /// <param name="routes"></param>
        void MapAllRoutes(RouteCollection routes);
    }
}