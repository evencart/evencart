using System;
using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Settings;

namespace RoastedMarketplace.Services.Settings
{
    public interface ISettingService : IFoundationEntityService<Setting>
    {
        Setting Get<T>(string keyName) where T : ISettingGroup;

        void Save<T>(string keyName, string keyValue, bool clearCache = false) where T : ISettingGroup;

        void Save<T>(T settings, bool clearCache = false) where T: ISettingGroup;

        void Save(Type settingType, object settings, bool clearCache = false);

        void Save(Type settingType, string keyName, string keyValue, bool clearCache = false);

        T GetSettings<T>() where T : ISettingGroup;

        void LoadSettings<T>(T settingsObject) where T : ISettingGroup;
    }
}