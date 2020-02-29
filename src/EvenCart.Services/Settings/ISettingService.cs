using System;
using EvenCart.Core.Config;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Settings;

namespace EvenCart.Services.Settings
{
    public interface ISettingService : IFoundationEntityService<Setting>
    {
        Setting Get<T>(string keyName, int storeId) where T : ISettingGroup;

        void Save<T>(string keyName, string keyValue, int storeId) where T : ISettingGroup;

        void Save<T>(T settings, int storeId) where T: ISettingGroup;

        void Save(Type settingType, object settings, int storeId);

        void Save(Type settingType, string keyName, string keyValue, int storeId);

        T GetSettings<T>(int storeId) where T : ISettingGroup;

        object GetSettings(Type settingType, int storeId);

        void LoadSettings<T>(T settingsObject, int storeId) where T : ISettingGroup;

        void DeleteSettings<T>(int storeId) where T : ISettingGroup;
    }
}