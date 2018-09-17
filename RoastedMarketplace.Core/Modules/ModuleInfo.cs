using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using RoastedMarketplace.Core.Infrastructure;

namespace RoastedMarketplace.Core.Modules
{
    public class ModuleInfo
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public IList<string> SupportedVersions { get; set; }

        public string Author { get; set; }

        public string AuthorUri { get; set; }

        public string ModuleUri { get; set; }

        public string AssemblyDllName { get; set; }

        public Assembly ShadowAssembly { get; set; }

        public FileInfo OriginalAssemblyFileInfo { get; set; }

        public Type ModuleType { get; set; }

        public bool IsSystemModule { get; set; }

        public bool IsEmbeddedModule { get; set; }

        public bool Installed { get; set; }

        public bool Active { get; set; }

        public static ModuleInfo Load(string fileName)
        {
            return ModuleConfigurator.LoadModuleInfo(fileName);
        }

        public static ModuleInfo Load(IModule module)
        {
            return ModuleConfigurator.LoadModuleInfo(module);
        }

        public T LoadModuleInstance<T>() where T : class, IModule
        {
            var instance = DependencyResolver.Resolve(this.ModuleType);
            var moduleTypedInstance = instance as T;
            return moduleTypedInstance;
        }
    }
}