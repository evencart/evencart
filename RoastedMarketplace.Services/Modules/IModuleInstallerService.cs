using RoastedMarketplace.Core.Modules;

namespace RoastedMarketplace.Services.Modules
{
    public interface IModuleInstallerService
    {
        void Install(ModuleInfo moduleInfo);

        void Uninstall(ModuleInfo moduleInfo);

        void Activate(ModuleInfo moduleInfo);

        void Deactivate(ModuleInfo moduleInfo);

    }
}