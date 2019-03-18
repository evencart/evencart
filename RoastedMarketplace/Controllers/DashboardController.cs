using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.ViewEngines;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Controllers
{
    [Route("")]
    public class DashboardController : FoundationController
    {
        private readonly IViewAccountant _viewAccountant;
        private readonly IUserService _userService;
        public DashboardController(IViewAccountant viewAccountant, IUserService userService)
        {
            _viewAccountant = viewAccountant;
            _userService = userService;
        }

        [DualGet("templates", Name = RouteNames.Templates, OnlyApi = true)]
        public IActionResult LoadTemplates(string context)
        {
            var templates = _viewAccountant.CompileAllViews(context, null, true);
            return R.Success.With("templates", templates).Result;
        }
        [DualPost("rawview", Name = RouteNames.RawView, OnlyApi = true)]
        public IActionResult GetRawView(string viewPath)
        {
            return R.Success.WithRawView(viewPath).Result;
        }
        [DualPost("set-active-currency", Name = RouteNames.SetActiveCurrency, OnlyApi = true)]
        public IActionResult SetActiveCurrency(int currencyId)
        {
            if (ApplicationEngine.CurrentUser == null)
                ApplicationEngine.GuestSignIn();

            ApplicationEngine.CurrentUser.ActiveCurrencyId = currencyId;
            _userService.Update(ApplicationEngine.CurrentUser);
            return R.Success.Result;
        }
    }
}