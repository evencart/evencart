using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Settings;

namespace RoastedMarketplace.Services.Settings
{
    public interface ISettingService : IFoundationEntityService<Setting>
    {
        Setting Get<T>(string keyName) where T : ISettingGroup;

        void Save<T>(string keyName, string keyValue) where T : ISettingGroup;

        void Save<T>(T settings) where T: ISettingGroup;

        T GetSettings<T>() where T : ISettingGroup;

        void LoadSettings<T>(T settingsObject) where T : ISettingGroup;
    }
}