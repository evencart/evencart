using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;

namespace RoastedMarketplace.Controllers
{
    [Route("component")]
    public class ComponentController : FoundationController
    {
        [HttpPost("{componentName}", Name = RouteNames.RenderComponent)]
        public IActionResult Index(string componentName)
        {
            return ViewComponent(componentName);
        }
    }
}