using System;
using EvenCart.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EvenCart
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return ApplicationEngine.ConfigureServices(services, _hostingEnvironment);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApplicationEngine.Configure(app, env);
        }
    }
}
