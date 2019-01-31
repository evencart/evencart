
using Microsoft.AspNetCore.Routing;

namespace RoastedMarketplace.Core.Infrastructure.Routing
{
    public partial class RouteData
    {
        public string RouteName { get; set; }

        public string Template { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public dynamic RouteValueDictionary { get; set; }

        public string SeoEntityName { get; set; }

        public int Order { get; set; }

        public string ParameterName { get; set; }

        public RouteValueDictionary GetRouteValueDictionary()
        {
            return (RouteValueDictionary) RouteValueDictionary;
        }
    }
}