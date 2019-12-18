using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EvenCartPluginDebugger
{
    public class Startup
    {
        private EvenCart.Startup startup;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            startup = new EvenCart.Startup(hostingEnvironment);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return startup.ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            startup.Configure(app, _hostingEnvironment);
        }
    }
}