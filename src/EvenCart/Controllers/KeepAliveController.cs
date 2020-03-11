#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;
using EvenCart.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class KeepAliveController : FoundationController
    {
        private readonly ILogger _logger;

        public KeepAliveController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("~/keep-alive", Name = RouteNames.KeepAlive)]
        public IActionResult Index()
        {
            _logger.LogInfo<KeepAliveController>(null, "Keep alive request received");
            return Content("");
        }
    }
}