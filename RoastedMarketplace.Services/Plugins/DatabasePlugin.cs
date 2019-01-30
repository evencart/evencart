using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Database;

namespace RoastedMarketplace.Services.Plugins
{
    public class DatabasePlugin : FoundationPlugin
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