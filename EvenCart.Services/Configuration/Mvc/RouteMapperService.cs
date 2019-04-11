using System;
using System.Web.Routing;
using RoastedMarketplace.Core.Exception;
using RoastedMarketplace.Core.Infrastructure.Routing;
using RoastedMarketplace.Core.Infrastructure.Utils;

namespace RoastedMarketplace.Services.Configuration.Mvc
{
    public class RouteMapperService : IRouteMapperService
    {
        public void MapAllRoutes(RouteCollection routes)
        {
            //we get all the instances of type IRouteMap
            var routeMaps = TypeFinder.ClassesOfType<IRouteMap>();
            foreach (var map in routeMaps)
            {
                //create instance of each map
                var mapInstance = Activator.CreateInstance(map) as IRouteMap;
                if (mapInstance == null)
                    throw new RoastedMarketplaceException(string.Format(
                        "Couldn't create instance of type {0} in namespace {1}", map.Name, map.Namespace));
                //and call the registration
                mapInstance.MapRoutes(routes);
            }

        }
    }
}