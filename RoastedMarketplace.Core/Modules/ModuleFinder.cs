using System.Collections.Generic;
using System.Linq;
using DryIoc;

namespace RoastedMarketplace.Core.Modules
{
    public static class ModuleFinder
    {
        public static ModuleInfo FindModule(string systemName)
        {
            var moduleInfo = ModuleEngine.Modules.FirstOrDefault(x => x.SystemName == systemName);
            if (moduleInfo != null)
            {
                if (moduleInfo.ModuleType == null)
                {
                    //we need to load the type of module
                    var allTypes = moduleInfo.ShadowAssembly.GetLoadedTypes();
                    moduleInfo.ModuleType = allTypes.FirstOrDefault(x => typeof(IModule).IsAssignableFrom(x));
                }
            }
            return moduleInfo;

        }

        public static IList<ModuleInfo> FindModules<T>() where T : IModule
        {
            return ModuleEngine.Modules.Where(x => typeof(T).IsAssignableFrom(x.ModuleType)).ToList();
        }
    }
}