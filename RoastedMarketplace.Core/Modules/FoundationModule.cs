using RoastedMarketplace.Core.Infrastructure.Routing;

namespace RoastedMarketplace.Core.Modules
{
    public abstract class FoundationModule : IModule
    {
        protected FoundationModule()
        {
        }

        public virtual ModuleInfo ModuleInfo { get; set; }

        /// <summary>
        /// A system module can't be deactivated or uninstalled. It's install method is called immediately on application restart
        /// </summary>
        public virtual bool IsSystemModule => false;

        public virtual bool IsEmbeddedModule { get; set; }

        public virtual void Install()
        {
            ModuleEngine.MarkInstalled(this.ModuleInfo);
        }

        public virtual void Uninstall()
        {
            ModuleEngine.MarkUninstalled(this.ModuleInfo);
        }

        public abstract RouteData GetConfigurationPageRouteData();

        public abstract RouteData GetDisplayPageRouteData();
        public abstract string SystemName { get; }
        public abstract string Guid { get; }
    }
}