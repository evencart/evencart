using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DynamicRouteAttribute : HttpGetAttribute
    {
        public DynamicRouteAttribute(string template) : base()
        {
            Template = template;
            Order = int.MaxValue;
        }

        public new string Template { get; }
    }
}