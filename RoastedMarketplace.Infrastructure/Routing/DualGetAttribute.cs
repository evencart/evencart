using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DualGetAttribute : HttpGetAttribute, IDualRouteAttribute
    {
        public DualGetAttribute(string template) : base(template)
        {
            
        }

        public bool OnlyApi { get; set; }
    }
}