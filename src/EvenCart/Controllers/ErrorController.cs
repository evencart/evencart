using EvenCart.Infrastructure.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    [Route("error")]
    public class ErrorController : FoundationController
    {
        public IActionResult Index(int? statusCode = null)
        {
            statusCode = statusCode ?? 404;
            return R.With("statusCode", statusCode).Result;
        }
    }
}