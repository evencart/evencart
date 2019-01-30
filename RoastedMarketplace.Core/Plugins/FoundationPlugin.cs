using System.Collections.Generic;
using DotEntity.Versioning;

namespace RoastedMarketplace.Core.Plugins
{
    public abstract class FoundationPlugin : IPlugin
    {
        public PluginInfo PluginInfo { get; set; }

        public virtual string ConfigurationUrl { get; } = null;

        /// <summary>
        /// A system module can't be deactivated or uninstalled. It's install method is called immediately on application restart
        /// </summary>
        public virtual bool IsSystemModule => false;

        public virtual void Install() { }

        public virtual void Uninstall() { }

        public virtual IList<IDatabaseVersion> GetDatabaseVersions()
        {
            return new List<IDatabaseVersion>();
        }
    }
}