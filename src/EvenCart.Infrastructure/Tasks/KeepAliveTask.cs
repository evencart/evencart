using System.Threading.Tasks;
using EvenCart.Core.Tasks;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.HttpServices;

namespace EvenCart.Infrastructure.Tasks
{
    public class KeepAliveTask : ITask
    {
        private readonly IRequestProvider _requestProvider;

        public KeepAliveTask(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task Run()
        {
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