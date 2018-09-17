using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure.Mvc;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class DashboardController : FoundationAdminController
    {
        public IActionResult Index()
        {
            return Result("Dashboard");
        }
    }
}