using System.Collections.Generic;
using DotEntity.Versioning;

namespace RoastedMarketplace.Core.Plugins
{
    public abstract class FoundationPlugin : IPlugin
    {
        public PluginInfo PluginInfo { get; set; }

        public virtual string ConfigurationUrl { get; } = null;

        public virtual void Install() { }

        public virtual void Uninstall() { }

        public virtual IList<IDatabaseVersion> GetDatabaseVersions()
        {
            return new List<IDatabaseVersion>();
        }
    }
}