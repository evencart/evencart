using System;
using System.Linq;
using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Settings;

namespace RoastedMarketplace.Services.Settings
{
    public class SettingService : FoundationEntityService<Setting>, ISettingService
    {
        public Setting Get<T>(string keyName) where T : ISettingGroup
        {
            var groupName = typeof(T).Name;
            var settings = Repository.Where(x => x.Key == keyName);
            if (!string.IsNullOrEmpty(groupName))
                settings = settings.Where(x => x.GroupName == groupName);

            return settings.SelectSingle();
        }

        public void Save<T>(string keyName, string keyValue) where T : ISettingGroup
        {
            var groupName = typeof(T).Name;

            //check if setting exist
            var setting = Get<T>(keyName);
            if (setting == null)
            {
                setting = new Setting() {
                    GroupName = groupName,
                    Key = keyName,
                    Value = keyValue
                };
                Insert(setting);
            }
            else
            {
                setting.Value = keyValue;
                Update(setting);
            }
        }

        public void Save<T>(T settings) where T : ISettingGroup
        {
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = typeof(T).GetProperties();
            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;
                var valueObj = property.GetValue(settings);
                var value = valueObj == null ? "" : valueObj.ToString();
                //save the property
                Save<T>(propertyName, value);
            }
        }

        public void Save(Type settingType, object settings)
        {
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = settingType.GetProperties();
            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;
                var valueObj = property.GetValue(settings);
                var value = valueObj == null ? "" : valueObj.ToString();
                //save the property
                Save(settingType, propertyName, value);
            }
        }

        public void Save(Type settingType, string keyName, string keyValue)
        {
            var groupName = settingType.Name;

            //check if setting exist
            var setting = Repository.Where(x => x.GroupName == groupName && x.Key == keyName).SelectSingle();
            if (setting == null)
            {
                setting = new Setting() {
                    GroupName = groupName,
                    Key = keyName,
                    Value = keyValue
                };
                Insert(setting);
            }
            else
            {
                setting.Value = keyValue;
                Update(setting);
            }
        }

        public T GetSettings<T>() where T : ISettingGroup
        {
            //create a new settings object
            var settingsObj = Activator.CreateInstance<T>();

            FurnishInstance(settingsObj);

            return settingsObj;
        }

        public void LoadSettings<T>(T settingsObject) where T : ISettingGroup
        {
            FurnishInstance(settingsObject);
        }

        private void FurnishInstance<T>(T settingsInstance) where T : ISettingGroup
        {
            var settingInstanceType = settingsInstance.GetType();
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = settingInstanceType.GetProperties();
            var allSettings = Repository.Where(x => x.GroupName == settingInstanceType.Name).Select().ToList();
            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;

                //retrive the value of setting from db
                var savedSettingEntity = allSettings.FirstOrDefault(x => x.Key == propertyName);

                if (savedSettingEntity != null)
                    //set the property
                    property.SetValue(settingsInstance, TypeConverter.CastPropertyValue(property, savedSettingEntity.Value));
            }
        }
    }
}