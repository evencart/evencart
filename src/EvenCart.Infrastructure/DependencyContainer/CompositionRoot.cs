#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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

            if (!ApplicationEngine.IsTestEnv())
            {
                //then the plugin ones
                var plugins = PluginLoader.GetAvailablePlugins().Where(x => x.DependencyContainer != null)
                    .OrderBy(x => x.DependencyContainer.Priority);
                foreach (var plugin in plugins)
                {
                    plugin.DependencyContainer.RegisterDependencies(registrar);
                    if (plugin.ActiveStoreIds?.Any() ?? false)
                        plugin.DependencyContainer.RegisterDependenciesIfActive(registrar);
                }
            }
        }
    }
}