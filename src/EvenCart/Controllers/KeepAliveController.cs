using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class KeepAliveController : FoundationController
    {
        [Route("~/keep-alive", Name = RouteNames.KeepAlive)]
        public IActionResult Index()
        {
            return Content("");
        }
    }
}