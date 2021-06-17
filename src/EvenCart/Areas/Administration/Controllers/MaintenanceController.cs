﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using Genesis;
using Genesis.Caching;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Modules.Logging;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class MaintenanceController : GenesisAdminController
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