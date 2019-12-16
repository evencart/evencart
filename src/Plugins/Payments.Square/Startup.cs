using EvenCart.Core.Startup;
using EvenCart.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace Payments.Square
{
    public class Startup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            //ignore the webhook url from antiforgery validation
            ApplicationConfig.IgnoreAntiforgeryValidation("/square/webhook");
        }

        public int Priority { get; }
    }
}