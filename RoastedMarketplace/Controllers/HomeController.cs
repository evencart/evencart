using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Cultures;

namespace RoastedMarketplace.Controllers
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