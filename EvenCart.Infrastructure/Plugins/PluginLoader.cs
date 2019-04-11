using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Plugins;
using EvenCart.Data.Extensions;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace EvenCart.Infrastructure.Plugins
{
    public class PluginLoader : IPluginLoader
    {
        private readonly string _pluginsDirectory;
        private readonly string _binDirectory;

        private readonly IHostingEnvironment _hostingEnvironment;
        public PluginLoader(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _binDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "bin", "plugins");
            _pluginsDirectory = Path.Combine(hostingEnvironment.ContentRootPath, "Plugins");
        }

        private static IList<PluginInfo> _loadedPlugins = null;
        public void LoadAvailablePlugins()
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
                //read the file content
                var fileContent = File.ReadAllText(file);
                PluginInfo pluginInfo;
                try
                {
                    pluginInfo = JsonConvert.DeserializeObject<PluginInfo>(fileContent);
                    if (pluginInfo == null)
                        continue;//invalid file found

                    //ignore any invalid ones
                    if (pluginInfo.SystemName.IsNullEmptyOrWhiteSpace() || pluginInfo.Name.IsNullEmptyOrWhiteSpace() ||
                        pluginInfo.AssemblyName.IsNullEmptyOrWhiteSpace())
                        continue;
                }
                catch
                {
                    //something must be wrong with the file content. Ignore the file
                    continue;
                }

                var fileInfo = new FileInfo(file);
                pluginInfo.PluginDirectory = fileInfo.DirectoryName;
                var assemblyPath = Path.Combine(pluginInfo.PluginDirectory, pluginInfo.AssemblyName);
                if (!File.Exists(assemblyPath))
                    throw new Exception(
                        $"Can't load the assembly {assemblyPath} for the plugin {pluginInfo.Name} ({pluginInfo.SystemName})");
                var mainAssemblyFileInfo = new FileInfo(assemblyPath);
                //copy the assembly file to bin directory
                SafeFileCopy(mainAssemblyFileInfo, Path.Combine(_binDirectory, mainAssemblyFileInfo.Name));
                var pluginAssembly = Assembly.LoadFile(Path.Combine(_binDirectory, mainAssemblyFileInfo.Name));
                //copy the dependent assemblies
                var dependentAssemblies = GetDependentAssemblies(pluginAssembly);
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
                _loadedPlugins.Add(pluginInfo);
            }
        }

        public IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false)
        {
            if (_loadedPlugins.All(x => x.Widgets == null) && withWidgets)
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

        public PluginInfo FindPlugin(string systemName)
        {
            return _loadedPlugins.FirstOrDefault(x => x.SystemName == systemName);
        }

        public void InitializeDependentAssemblyResolver()
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

        public bool SafeFileCopy(FileInfo sourceFileInfo, string targetPath)
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

        private IDictionary<string, string> GetDependentAssemblies(Assembly analyzedAssembly)
        {
            var referencedAssemblies = analyzedAssembly.GetReferencedAssemblies();
            var dependentFileNames = referencedAssemblies.ToDictionary(x => x.FullName, x => $"{x.Name}.dll");
            return dependentFileNames;
        }

        public IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies()
                .Select(assemblyName => assemblyName.FullName);
        }
    }
}