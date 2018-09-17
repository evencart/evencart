
using Microsoft.AspNetCore.Routing;

namespace RoastedMarketplace.Core.Infrastructure.Routing
{
    public partial class RouteData
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public dynamic RouteValueDictionary { get; set; }

        public RouteValueDictionary GetRouteValueDictionary()
        {
            return (RouteValueDictionary) RouteValueDictionary;
        }
    }
}