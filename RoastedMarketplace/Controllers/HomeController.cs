using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;

namespace RoastedMarketplace.Controllers
{
    public class HomeController : FoundationController
    {
        [HttpGet("~/", Name = RouteNames.Home)]
        public IActionResult Index()
        {
            return Content("Home");
        }
    }
}