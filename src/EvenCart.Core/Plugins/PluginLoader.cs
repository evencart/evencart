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
using System.Linq;
using System.Reflection;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Startup;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace EvenCart.Core.Plugins
{
    public static class PluginLoader
    {
        private static string _pluginsDirectory;
        private static string _binDirectory;

        public static void Init(IHostingEnvironment hostingEnvironment)
        {
            _binDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "bin", "plugins");
            _pluginsDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "Plugins");
        }

        private static IList<PluginInfo> _loadedPlugins = null;
        public static void LoadAvailablePlugins()
        {
            if (_loadedPlugins != null)
                return;
            InitializeDependentAssemblyResolver();
            _loadedPlugins = new List<PluginInfo>();
            if (!Directory.Exists(_pluginsDirectory))
                return;
            if (!Directory.Exists(_binDirectory))
                Directory.CreateDirectory(_binDirectory);

            var files = Directory.GetFiles(_pluginsDirectory, "config.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                LoadPluginFromConfig(file);
            }
        }

        public static PluginInfo LoadPluginFromConfig(string configFile, bool pendingRestart = false)
        {
            //read the file content
            var fileContent = File.ReadAllText(configFile);
            PluginInfo pluginInfo;
            try
            {
                pluginInfo = JsonConvert.DeserializeObject<PluginInfo>(fileContent);
                if (pluginInfo == null)
                    return null;//invalid file found

                //ignore any invalid ones
                if (IsNullEmptyOrWhitespace(pluginInfo.SystemName) || IsNullEmptyOrWhitespace(pluginInfo.Name) || IsNullEmptyOrWhitespace(pluginInfo.AssemblyName))
                    return null;
                //is this plugin version supported?
                if (pluginInfo.SupportedVersions.All(x => !AppVersionEvaluator.IsVersionSupported(x)))
                    return null;
            }
            catch
            {
                //something must be wrong with the file content. Ignore the file
                return null;
            }

            var fileInfo = new FileInfo(configFile);
            pluginInfo.PluginDirectory = fileInfo.DirectoryName;
            var assemblyPath = Path.Combine(pluginInfo.PluginDirectory, pluginInfo.AssemblyName);
            if (!File.Exists(assemblyPath))
                throw new System.Exception(
                    $"Can't load the assembly {assemblyPath} for the plugin {pluginInfo.Name} ({pluginInfo.SystemName})");
            var mainAssemblyFileInfo = new FileInfo(assemblyPath);
            //copy the assembly file to bin directory
            SafeFileCopy(mainAssemblyFileInfo, Path.Combine(_binDirectory, mainAssemblyFileInfo.Name));
            var pluginAssembly = Assembly.LoadFile(Path.Combine(_binDirectory, mainAssemblyFileInfo.Name));
            //copy the dependent assemblies
            var dependentAssemblies = GetPluginDirectoryDlls(pluginInfo); //GetDependentAssemblies(pluginAssembly);
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var da in dependentAssemblies)
            {
                if (loadedAssemblies.Any(x => x.FullName == da.Key))
                    continue;
                var dAssemblyPath = Path.Combine(pluginInfo.PluginDirectory, da.Value);
                if (!File.Exists(dAssemblyPath))
                    continue;
                var assemblyFileInfo = new FileInfo(dAssemblyPath);
                var targetPath = Path.Combine(_binDirectory, da.Value);
                SafeFileCopy(assemblyFileInfo, targetPath);
            }
            //set the assembly to the copied one
            pluginInfo.Assembly = pluginAssembly;
            pluginInfo.PluginType = TypeFinder.OfType<IPlugin>(pluginAssembly);

            //set startup
            var startupType = TypeFinder.OfType<IAppStartup>(pluginAssembly);
            if (startupType != null)
                pluginInfo.Startup = (IAppStartup)Activator.CreateInstance(startupType);

            //set container
            var diType = TypeFinder.OfType<IDependencyContainer>(pluginAssembly);
            if (diType != null)
                pluginInfo.DependencyContainer = (IDependencyContainer)Activator.CreateInstance(diType);

            _loadedPlugins.Add(pluginInfo);
            pluginInfo.PendingRestart = pendingRestart;

            return pluginInfo;
        }

        public static IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false)
        {
            if (_loadedPlugins != null && _loadedPlugins.All(x => x.Widgets == null) && withWidgets)
            {
                foreach (var pluginInfo in _loadedPlugins)
                {
                    var widgetTypes = TypeFinder.ClassesOfType<IWidget>(pluginInfo.Assembly);
                    var widgets = widgetTypes.Where(x => x != null).Select(x => (IWidget)DependencyResolver.ResolveOptional(x)).ToList();
                    pluginInfo.Widgets = widgets;
                }
            }
            return _loadedPlugins;
        }

        public static PluginInfo FindPlugin(string systemName)
        {
            return _loadedPlugins.FirstOrDefault(x => x.SystemName == systemName);
        }

        public static void InitializeDependentAssemblyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                var pluginDependencyName = e.Name.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First();

                var pluginDependencyFullName =
                    Path.Combine(_binDirectory, $"{pluginDependencyName}.dll"
                    );

                return
                    File.Exists(pluginDependencyFullName)
                        ? Assembly.LoadFile(pluginDependencyFullName)
                        : null;
            };
        }

        public static bool SafeFileCopy(FileInfo sourceFileInfo, string targetPath)
        {
            try
            {
                sourceFileInfo.CopyTo(targetPath, true);
                return true;
            }
            catch (IOException ex)
            {
                //It's quite possible that the file is locked and being used by other process.
                //but it allows renaming...weird...lets try
                var renamedFileName = targetPath + "." + Guid.NewGuid().ToString() + ".exclude";
                try
                {
                    File.Move(targetPath, renamedFileName);
                }
                catch (IOException)
                {
                    return false;
                }
                //if we are here, let's try to copy again
                sourceFileInfo.CopyTo(targetPath, true);
                return true;
            }
        }

        private static IDictionary<string, string> GetDependentAssemblies(Assembly analyzedAssembly)
        {
            var referencedAssemblies = analyzedAssembly.GetReferencedAssemblies();
            var dependentFileNames = referencedAssemblies.ToDictionary(x => x.FullName, x => $"{x.Name}.dll");
            return dependentFileNames;
        }

        private static IDictionary<string, string> GetPluginDirectoryDlls(PluginInfo pluginInfo)
        {
            var dllFiles = Directory.GetFiles(pluginInfo.PluginDirectory, "*.dll");
            var fileInfos = dllFiles.Where(x => x != pluginInfo.SystemName + ".dll").Select(x => new FileInfo(x));
            return fileInfos.ToDictionary(x => x.FullName, x => x.Name);
        }
        private static bool IsNullEmptyOrWhitespace(string s)
        {
            return string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s);
        }
        public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies()
                .Select(assemblyName => assemblyName.FullName);
        }
    }
}