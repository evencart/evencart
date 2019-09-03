using EvenCart.Core;
using EvenCart.Data.Constants;
using EvenCart.Infrastructure.Caching;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class MaintenanceController : FoundationAdminController
    {
        private readonly ICacheAccountant _cacheAccountant;
        private readonly ILogger _logger;
        public MaintenanceController(ICacheAccountant cacheAccountant, ILogger logger)
        {
            _cacheAccountant = cacheAccountant;
            _logger = logger;
        }

        [DualPost("restart", Name = AdminRouteNames.RestartApplication, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult RestartApplication()
        {
            ServerHelper.RestartApplication();
            _logger.LogInfo<MaintenanceController>(null, "Application pool restarted successfully!", null);
            return R.Success.Result;
        }

        [DualPost("purge", Name = AdminRouteNames.PurgeCache, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.PerformMaintenance)]
        public IActionResult PurgeCache()
        {
            _cacheAccountant.PurgeCache();
            _logger.LogInfo<MaintenanceController>(null, "Application cache purged successfully!", null);
            return R.Success.Result;
        }
    }
}