using RoastedMarketplace.Core.Modules;

namespace RoastedMarketplace.Services.Modules
{
    public class ModuleInstallerService : IModuleInstallerService
    {
        public void Install(ModuleInfo moduleInfo)
        {
            if (moduleInfo.Installed)
                return;

            var moduleType = moduleInfo.LoadModuleInstance<IModule>();
            if(moduleType != null)
                moduleType.Install();
        }

        public void Uninstall(ModuleInfo moduleInfo)
        {
            if (!moduleInfo.Installed)
                return;

            var moduleType = moduleInfo.LoadModuleInstance<IModule>();
            if (moduleType != null)
                moduleType.Uninstall();
        }

        public void Activate(ModuleInfo moduleInfo)
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate(ModuleInfo moduleInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}