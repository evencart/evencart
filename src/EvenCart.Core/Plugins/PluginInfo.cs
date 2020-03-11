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