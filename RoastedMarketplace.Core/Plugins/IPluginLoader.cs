using System.Collections.Generic;

namespace RoastedMarketplace.Core.Plugins
{
    public interface IPluginLoader
    {
        void LoadAvailablePlugins();

        IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false);

        PluginInfo FindPlugin(string systemName);
    }
}