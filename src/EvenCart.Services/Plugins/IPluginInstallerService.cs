using EvenCart.Core.Plugins;

namespace EvenCart.Services.Plugins
{
    public interface IPluginInstallerService
    {
        void Install(PluginInfo moduleInfo);

        void Uninstall(PluginInfo moduleInfo);
    }
}