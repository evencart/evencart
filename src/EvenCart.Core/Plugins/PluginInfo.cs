using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Startup;

namespace EvenCart.Core.Plugins
{
    public class PluginInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string SystemName { get; set; }

        public IList<string> SupportedVersions { get; set; }

        public string Author { get; set; }

        public string AuthorUri { get; set; }

        public string PluginUri { get; set; }

        public string AssemblyName { get; set; }

        public string PluginDirectory { get; set; }

        public Assembly Assembly { get; set; }

        public FileInfo OriginalAssemblyFileInfo { get; set; }

        public Type PluginType { get; set; }

        public bool Installed { get; set; }

        [Obsolete("Use ActiveStoreIds instead", true)]
        public bool Active { get; set; }

        public IList<int> ActiveStoreIds { get; set; }

        public bool Dirty { get; set; }

        public bool PendingRestart { get; set; }

        public IList<IWidget> Widgets { get; set; }

        public IAppStartup Startup { get; set; }

        public IDependencyContainer DependencyContainer { get; set; }

        public string ConfigurationUrl => LoadPluginInstance<IPlugin>()?.ConfigurationUrl;

        public T LoadPluginInstance<T>() where T : class, IPlugin
        {
            if (this.PendingRestart)
                return default(T);
            var instance = DependencyResolver.Resolve(this.PluginType);
            var pluginTypedInstance = instance as T;
            if (pluginTypedInstance != null)
                pluginTypedInstance.PluginInfo = this;
            return pluginTypedInstance;
        }
    }
}