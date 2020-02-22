using System.Collections.Generic;
using System.Linq;

namespace EvenCart.Core.Plugins
{
    public static class PluginFinder
    {
        public static PluginInfo FindPlugin(string systemName)
        {
            var moduleInfo = PluginLoader.FindPlugin(systemName);
            return moduleInfo;
        }

        public static IList<PluginInfo> FindPlugins<T>() where T : IPlugin
        {
            var availablePlugins = PluginLoader.GetAvailablePlugins();
            return availablePlugins.Where(x => typeof(T).IsAssignableFrom(x.PluginType)).ToList();
        }
    }
}