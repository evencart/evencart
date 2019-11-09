using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EvenCart.Core.Startup
{
    public interface IAppStartup
    {
        void ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment);

        void Configure(IApplicationBuilder app);

        /// <summary>
        /// The priority of task. Lower means earlier in pipeline
        /// </summary>
        int Priority { get; }
    }
}