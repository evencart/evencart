using System.Web.Http;
using Owin;

namespace RoastedMarketplace.Core.Startup
{
    /// <summary>
    /// Task for running owin startup tasks
    /// </summary>
    public interface IOwinStartupTask
    {
        void Configuration(IAppBuilder app, HttpConfiguration configuration);

        int Priority { get; }
    }
}