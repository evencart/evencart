using RoastedMarketplace.Core.Infrastructure.Routing;

namespace RoastedMarketplace.Core.Plugins
{
    public abstract class FoundationPlugin : IPlugin
    {
        protected FoundationPlugin()
        {
        }

        public virtual PluginInfo ModuleInfo { get; set; }

        /// <summary>
        /// A system module can't be deactivated or uninstalled. It's install method is called immediately on application restart
        /// </summary>
        public virtual bool IsSystemModule => false;

        public virtual void Install() { }

        public virtual void Uninstall() { }

        public abstract RouteData GetConfigurationPageRouteData();

        public abstract RouteData GetDisplayPageRouteData();
    }
}