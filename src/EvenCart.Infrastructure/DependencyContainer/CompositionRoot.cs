using System.Linq;
using DryIoc;
using EvenCart.Core.Plugins;

namespace EvenCart.Infrastructure.DependencyContainer
{
    public class CompositionRoot
    {
        public CompositionRoot(IContainer registrar)
        {
            var coreRegistrar = new DependencyContainer();
            coreRegistrar.RegisterDependencies(registrar);

            //then the plugin ones
            var plugins = PluginLoader.GetAvailablePlugins().Where(x => x.DependencyContainer != null)
                .OrderBy(x => x.DependencyContainer.Priority);
            foreach (var plugin in plugins)
            {
                plugin.DependencyContainer.RegisterDependencies(registrar);
                if (plugin.Active)
                    plugin.DependencyContainer.RegisterDependenciesIfActive(registrar);
            }
        }
    }
}