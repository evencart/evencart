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
    }
}