using System.Collections.Generic;
using EvenCart.Core.Plugins;
using EvenCart.Data.Database;

namespace EvenCart.Services.Plugins
{
    public abstract class DatabasePlugin : FoundationPlugin
    {
        public override void Install()
        {
            DatabaseManager.UpgradeDatabase(PluginInfo.SystemName);
        }

        public override void Uninstall()
        {
            DatabaseManager.CleanupDatabase(PluginInfo.SystemName);
        }

        public virtual bool IsDatabaseUpgradeRequired(IList<string> installedVersions)
        {
            return false;
        }
    }
}