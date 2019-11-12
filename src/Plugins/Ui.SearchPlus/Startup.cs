using EvenCart.Core.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ui.SearchPlus.Middleware;

namespace Ui.SearchPlus
{
    public class Startup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            //do nothing
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<SearchTermTrackerMiddleware>();
        }

        public int Priority { get; } = 0;
    }
}