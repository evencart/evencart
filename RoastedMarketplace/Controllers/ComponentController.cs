using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure;
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
            //pass any additional data as model to the view
            var model = ApplicationEngine.CurrentHttpContext.Request.Form?.ToDictionary(x => x.Key, x => (object) x.Value);
            return ViewComponent(componentName, model);
        }
    }
}