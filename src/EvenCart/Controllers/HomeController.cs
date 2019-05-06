using System.Threading.Tasks;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class HomeController : FoundationController
    {
       [HttpGet("~/", Name = RouteNames.Home)]
        public async Task<IActionResult> Index()
        {
            return R.Success.Result;
        }
    }
}