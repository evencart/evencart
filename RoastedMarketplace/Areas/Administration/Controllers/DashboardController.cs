using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.ViewEngines;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class DashboardController : FoundationAdminController
    {
        private readonly IViewAccountant _viewAccountant;
        public DashboardController(IViewAccountant viewAccountant)
        {
            _viewAccountant = viewAccountant;
        }

        public IActionResult Index()
        {
            return Result();
        }

        [DualGet("templates/get", Name = AdminRouteNames.GetTemplates, OnlyApi = true)]
        public IActionResult LoadTemplates(string context)
        {
            var templates = _viewAccountant.CompileAllViews(context);
            return R.Success.With("templates", templates).Result;
        }
    }
}