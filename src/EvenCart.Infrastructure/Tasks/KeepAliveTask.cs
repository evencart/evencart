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

using System.Threading.Tasks;
using EvenCart.Core.Tasks;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;
using EvenCart.Services.HttpServices;
using EvenCart.Services.Logger;

namespace EvenCart.Infrastructure.Tasks
{
    public class KeepAliveTask : ITask
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ILogger _logger;
        public KeepAliveTask(IRequestProvider requestProvider, ILogger logger)
        {
            _requestProvider = requestProvider;
            _logger = logger;
        }

        public async Task Run()
        {
            _logger.LogInfo<KeepAliveTask>(null, "Starting a keep alive request");
            await _requestProvider.GetStringAsync(ApplicationEngine.RouteUrl(RouteNames.KeepAlive, null, true));
        }

        public string SystemName => "EvenCart.Infrastructure.Tasks.KeepAliveTask";

        public string Name => "Keep Alive";

        public int DefaultCycleDurationInSeconds => 20 * 60; //once every 20 minutes

        public void Dispose()
        {
            //do nothing
        }
    }
}