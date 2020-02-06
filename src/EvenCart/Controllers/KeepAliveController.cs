using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class KeepAliveController : FoundationController
    {
        private readonly ILogger _logger;

        public KeepAliveController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("~/keep-alive", Name = RouteNames.KeepAlive)]
        public IActionResult Index()
        {
            _logger.LogInfo<KeepAliveController>(null, "Keep alive request received");
            return Content("");
        }
    }
}