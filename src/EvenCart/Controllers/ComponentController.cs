using System.Linq;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
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