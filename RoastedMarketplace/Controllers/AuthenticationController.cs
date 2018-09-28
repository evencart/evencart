using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Authentication;

namespace RoastedMarketplace.Controllers
{
    public class AuthenticationController : FoundationController
    {
        private readonly IAppAuthenticationService _appAuthenticationService;
        public AuthenticationController(IAppAuthenticationService appAuthenticationService)
        {
            _appAuthenticationService = appAuthenticationService;
        }

        [DualGet("login", Name = RouteNames.Login)]
        public IActionResult Login()
        {
            return Result();
        }

        [DualPost("login", Name = RouteNames.Login)]
        public IActionResult Login(string userName, string password)
        {
            if (userName == "test" && password == "test")
            {
                var loginStatus = _appAuthenticationService.SignIn("test@test.com", forceCreateNewAccount: true);
            }

            return Result(Login(), Json(new { }));
        }
    }
}