using EvenCart.Core;
using EvenCart.Data.Constants;
using EvenCart.Infrastructure.Caching;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class MaintenanceController : FoundationAdminController
    {
        private readonly ICacheAccountant _cacheAccountant;
        public MaintenanceController(ICacheAccountant cacheAccountant)
        {
            _cacheAccountant = cacheAccountant;
        }

        [DualPost("restart", Name = AdminRouteNames.RestartApplication, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult RestartApplication()
        {
            ServerHelper.RestartApplication();
            return R.Success.Result;
        }

        [DualPost("purge", Name = AdminRouteNames.PurgeCache, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult PurgeCache()
        {
            _cacheAccountant.PurgeCache();
            return R.Success.Result;
        }
    }
}