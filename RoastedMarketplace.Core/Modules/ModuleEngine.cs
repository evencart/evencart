using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using RoastedMarketplace.Core.Infrastructure.Utils;

namespace RoastedMarketplace.Core.Modules
{
    public class ModuleEngine
    {
        private const string ModulesFile = "~/App_Data/Modules.config";

        private static string _modulesDirectory = "~/Modules";
        private static string _shadowDirectory = "~/Modules/loaded"; //this is set in web.config as probe path //used only for medium trust

        public static IList<ModuleInfo> Modules { get; set; }

        private static readonly DirectoryInfo ModulesDirectory;

        private static readonly DirectoryInfo ShadowDirectory;

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        static ModuleEngine()
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
            Modules = new List<ModuleInfo>();
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
            var allTypes = TypeFinder.ClassesOfType<IModule>();
            foreach (var type in allTypes)
            {
                var instance = (IModule)Activator.CreateInstance(type);
                Modules.Add(ModuleInfo.Load(instance));
            }

        }

        /// <summary>
        /// Marks a module as installed
        /// </summary>
        /// <param name="moduleInfo"></param>
        public static void MarkInstalled(ModuleInfo moduleInfo)
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
        public static void MarkUninstalled(ModuleInfo moduleInfo)
        {
            /*
            moduleInfo.Installed = false;
            moduleInfo.Active = false;
            ModuleConfigurator.WriteConfigFile(Modules.ToList(), HostingEnvironment.MapPath(ModulesFile));
            */
        }

        public static bool IsInstalled(ModuleInfo moduleInfo)
        {
            return Modules.Any(x => x.SystemName == moduleInfo.SystemName);
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
    }
}