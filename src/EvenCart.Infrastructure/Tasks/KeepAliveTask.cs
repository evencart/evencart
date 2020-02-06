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