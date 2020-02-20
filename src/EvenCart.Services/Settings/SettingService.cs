using System;
using System.Linq;
using EvenCart.Core.Config;
using EvenCart.Core.Extensions;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Enum;
using EvenCart.Services.Logger;
using EvenCart.Services.Serializers;

namespace EvenCart.Services.Settings
{
    public class SettingService : FoundationEntityService<Setting>, ISettingService
    {
        private readonly IDataSerializer _dataSerializer;
        private readonly ILogger _logger;
        public SettingService(IDataSerializer dataSerializer, ILogger logger)
        {
            _dataSerializer = dataSerializer;
            _logger = logger;
        }

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
            Save(typeof(T), keyName, keyValue);
          
        }

        public void Save<T>(T settings) where T : ISettingGroup
        {
            Save(typeof(T), settings);
        }

        public void Save(Type settingType, object settings)
        {
            var propertyFields = settingType.GetProperties();
            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;
                var valueObj = property.GetValue(settings);
                var value = "";
                if (!property.PropertyType.IsPrimitive())
                {
                    value = valueObj != null ? _dataSerializer.Serialize(valueObj) : "";
                }
                else
                {
                    value = valueObj != null ? valueObj.ToString() : "";
                }
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

        public void DeleteSettings<T>() where T : ISettingGroup
        {
            var groupName = typeof(T).Name;
            Delete(x => x.GroupName == groupName);
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

                //retrieve the value of setting from db
                var savedSettingEntity = allSettings.FirstOrDefault(x => x.Key == propertyName);

                if (savedSettingEntity != null)
                {
                    try
                    {
                        property.SetValue(settingsInstance,
                            property.PropertyType.IsPrimitive()
                                ? TypeConverter.CastPropertyValue(property, savedSettingEntity.Value)
                                : _dataSerializer.Deserialize(savedSettingEntity.Value, property.PropertyType));
                    }
                    catch(Exception ex)
                    {
                        _logger.Log<Setting>(LogLevel.Error,
                            $"Invalid value '{savedSettingEntity.Value}' for setting '{settingInstanceType.Name}.{property.Name}'",
                            ex);
                    }
                   
                }
            }
        }
    }
}