using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.ViewEngines;

namespace RoastedMarketplace.Controllers
{
    [Route("dashboard")]
    public class DashboardController : FoundationController
    {
        private readonly IViewAccountant _viewAccountant;
        public DashboardController(IViewAccountant viewAccountant)
        {
            _viewAccountant = viewAccountant;
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
    }
}