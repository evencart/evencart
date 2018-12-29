using System.Collections.Generic;

namespace RoastedMarketplace.Core.Plugins
{
    public interface IPluginLoader
    {
        void LoadAvailablePlugins();

        IList<PluginInfo> GetAvailablePlugins();

        PluginInfo FindPlugin(string systemName);
    }
}