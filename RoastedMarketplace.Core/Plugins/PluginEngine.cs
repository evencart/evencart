using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using RoastedMarketplace.Core.Infrastructure.Utils;

namespace RoastedMarketplace.Core.Plugins
{
    public class PluginEngine
    {
        private const string PluginsFile = "~/App_Data/plugins.config";

        private static string _modulesDirectory = "~/Modules";
        private static string _shadowDirectory = "~/Modules/loaded"; //this is set in web.config as probe path //used only for medium trust

        public static IList<PluginInfo> Plugins { get; set; }

        private static readonly DirectoryInfo PluginsDirectory;

        private static readonly DirectoryInfo ShadowDirectory;

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        static PluginEngine()
        {
            /*
            TrustLevel = ServerHelper.GetCurrentTrustLevel();
            if (TrustLevel == AspNetHostingPermissionLevel.Unrestricted)
            {
                _shadowDirectory = AppDomain.CurrentDomain.DynamicDirectory;
                ShadowDirectory = new DirectoryInfo(_shadowDirectory);
            }
            else
            {
                ShadowDirectory = new DirectoryInfo(HostingEnvironment.MapPath(_shadowDirectory));

            }

            ModulesDirectory = new DirectoryInfo(HostingEnvironment.MapPath(_modulesDirectory));
            */
            Plugins = new List<PluginInfo>();
            LoadAssemblySystemModules();
        }
        /// <summary>
        /// Initializes the module engine for the application
        /// </summary>
        public static void Initialize()
        {
            /*
            using (new RoastedMarketplaceLocker(Locker))
            {
                //first we load the assemblies which have been compiled into
                LoadAssemblySystemModules();

                //now the other modules
                //first read the modules list
                var allModulesSystemNames = ModuleConfigurator.ReadConfigFile(HostingEnvironment.MapPath(ModulesFile));

                //create shadow directory
                Directory.CreateDirectory(_shadowDirectory);

                //delete all files from shadow directory if it's /loaded directory
                if (TrustLevel != AspNetHostingPermissionLevel.Unrestricted)
                {
                    var allFiles = ShadowDirectory.GetFiles("*", SearchOption.AllDirectories);
                    foreach (var file in allFiles)
                    {
                        file.Delete();
                    }
                }
                //make sure that we don't have null for all modulenames
                allModulesSystemNames = allModulesSystemNames ?? new List<ModuleConfigurator.ReadModuleInfo>();

                //load modules
                var allModules = ModuleConfigurator.GetAllModulesInfos(ModulesDirectory, allModulesSystemNames.ToList());
                foreach (var module in allModules)
                {
                    //discard the module which is incompatible
                    if (!module.SupportedVersions.Contains(GlobalSettings.AppVersion))
                        continue;

                    if (string.IsNullOrEmpty(module.SystemName))
                        throw new RoastedMarketplaceException(
                            $"A module with empty system name {module.Name}({module.OriginalAssemblyFileInfo.FullName}) can't be loaded");
                    //is this module already there
                    if (Modules.Any(x => x.SystemName == module.SystemName))
                        throw new RoastedMarketplaceException(
                            $"A module with same name {module.Name}({module.SystemName}) has already been loaded");

                    //add to module list
                    Modules.Add(module);

                    //if module is installed, copy it's dlls to shadow
                    //if (!module.Installed) 
                    //    continue;

                    //load to build manager
                    if (module.OriginalAssemblyFileInfo.Directory == null)
                        continue;

                    var allDlls = module.OriginalAssemblyFileInfo.Directory.GetFiles("*.dll",
                        SearchOption.AllDirectories);

                    //copy all the dlls to shadow directory
                    foreach (var dllFile in allDlls)
                    {
                        var shadowedFilePath = Path.Combine(ShadowDirectory.FullName, dllFile.Name);
                        if (EnsuredFileCopy(dllFile, shadowedFilePath))
                        {
                            var shadow = Assembly.LoadFrom(shadowedFilePath);
                            if (dllFile.Name == module.OriginalAssemblyFileInfo.Name)
                                module.ShadowAssembly = shadow;

                            //add this assembly to buildmanager to load
                            AssemblyLoader.AddToBuildManager(shadow);
                        }
                        else
                        {
                            throw new IOException($"Failed to copy {dllFile.Name} to plugins directory. Plugin initialization failed.");
                        }

                    }
                }
            } //dispose locker
            */
        }
        /// <summary>
        /// Loads the system modules compiled into the assembly
        /// </summary>
        public static void LoadAssemblySystemModules()
        {
            var allTypes = TypeFinder.ClassesOfType<IPlugin>();
            foreach (var type in allTypes)
            {
                var instance = (IPlugin)Activator.CreateInstance(type);
                Plugins.Add(PluginInfo.Load(instance));
            }

        }

        /// <summary>
        /// Marks a module as installed
        /// </summary>
        /// <param name="moduleInfo"></param>
        public static void MarkInstalled(PluginInfo moduleInfo)
        {
            /*
            moduleInfo.Installed = true;
            moduleInfo.Active = true;
            ModuleConfigurator.WriteConfigFile(Modules.ToList(), HostingEnvironment.MapPath(ModulesFile));
            */
        }
        /// <summary>
        /// Marks a module as uninstalled
        /// </summary>
        /// <param name="moduleInfo"></param>
        public static void MarkUninstalled(PluginInfo moduleInfo)
        {
            /*
            moduleInfo.Installed = false;
            moduleInfo.Active = false;
            ModuleConfigurator.WriteConfigFile(Modules.ToList(), HostingEnvironment.MapPath(ModulesFile));
            */
        }

        public static bool IsInstalled(PluginInfo moduleInfo)
        {
            return Plugins.Any(x => x.SystemName == moduleInfo.SystemName);
        }

        public static bool EnsuredFileCopy(FileInfo sourceFileInfo, string targetPath)
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

        private static IEnumerable<Assembly> GetPluginAssemblies(string pluginsRootPath)
        {
            if (!Directory.Exists(pluginsRootPath))
            {
                yield break;
            }

            var pluginDirectories = Directory.GetDirectories(pluginsRootPath);
            foreach (var pluginDirectory in pluginDirectories)
            {
                var pluginFullName =
                    Path.Combine(
                        pluginsRootPath,
                        $"{Path.GetFileName(pluginDirectory)}.dll"
                    );

                if (File.Exists(pluginFullName))
                {
                    yield return Assembly.LoadFile(pluginFullName);
                }
            }
        }
    }
}