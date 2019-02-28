using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Controllers
{
    [Route("error")]
    public class ErrorController : FoundationController
    {
        public IActionResult Index(int? statusCode = null)
        {
            statusCode = statusCode ?? 404;
            if (statusCode == 404)
                return R.WithView("404").Result;
            return R.Result;
        }
    }
}