using RoastedMarketplace.Core.Plugins;

namespace RoastedMarketplace.Services.Plugins
{
    public class PluginInstallerService : IPluginInstallerService
    {

        public void Install(PluginInfo pluginInfo)
        {
            if (pluginInfo.Installed)
                return;

            var moduleType = pluginInfo.LoadPluginInstance<IPlugin>();
            if(moduleType != null)
                moduleType.Install();

            pluginInfo.Installed = true;
        }

        public void Uninstall(PluginInfo pluginInfo)
        {
            if (!pluginInfo.Installed)
                return;

            var moduleType = pluginInfo.LoadPluginInstance<IPlugin>();
            if (moduleType != null)
                moduleType.Uninstall();
            pluginInfo.Installed = false;
        }
    }
}