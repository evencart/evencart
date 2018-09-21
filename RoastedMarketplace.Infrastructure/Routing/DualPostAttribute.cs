using Microsoft.AspNetCore.Mvc;

namespace RoastedMarketplace.Infrastructure.Routing
{
    public class DualPostAttribute : HttpPostAttribute, IDualRouteAttribute
    {
        public DualPostAttribute(string template) : base(template)
        {

        }

        public bool OnlyApi { get; set; }
    }
}