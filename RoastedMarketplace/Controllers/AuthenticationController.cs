using System.Collections.Generic;
using DotLiquid;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Shop;
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
            var products = new List<Product>()
            {
                new Product() {Name = "First Product"},
                new Product() {Name = "Second Product"},
                new Product() {Name = "Third Product"},
            };
            Template.RegisterSafeType(typeof(Product), new []{ "Name"});
            return Result("Authentication/Login", new {products});
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