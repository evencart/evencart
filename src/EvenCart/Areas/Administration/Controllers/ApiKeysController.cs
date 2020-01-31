using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class ApiKeysController : FoundationAdminController
    {
        [DualGet("", Name = AdminRouteNames.ApiKeysList)]
        public IActionResult ApiKeysList()
        {

        }

        public IActionResult DeleteApiKey()
        {

        }

        public IActionResult SaveApiKey()
        {

        }
    }
}