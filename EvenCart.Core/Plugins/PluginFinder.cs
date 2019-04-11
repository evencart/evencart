using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;

namespace EvenCart.Core.Plugins
{
    public static class PluginFinder
    {
        public static PluginInfo FindPlugin(string systemName)
        {
            var pluginLoader = DependencyResolver.Resolve<IPluginLoader>();
            var moduleInfo = pluginLoader.FindPlugin(systemName);
            return moduleInfo;
        }

        public static IList<PluginInfo> FindPlugins<T>() where T : IPlugin
        {
            var pluginLoader = DependencyResolver.Resolve<IPluginLoader>();
            var availablePlugins = pluginLoader.GetAvailablePlugins();
            return availablePlugins.Where(x => typeof(T).IsAssignableFrom(x.PluginType)).ToList();
        }
    }
}