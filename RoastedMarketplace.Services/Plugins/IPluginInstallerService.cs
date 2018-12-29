using RoastedMarketplace.Core.Plugins;

namespace RoastedMarketplace.Services.Plugins
{
    public interface IPluginInstallerService
    {
        void Install(PluginInfo moduleInfo);

        void Uninstall(PluginInfo moduleInfo);
    }
}