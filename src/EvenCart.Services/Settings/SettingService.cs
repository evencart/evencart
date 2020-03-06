#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Linq;
using EvenCart.Core.Caching;
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
        private readonly ICacheProvider _cacheProvider;
        public SettingService(IDataSerializer dataSerializer, ILogger logger, ICacheProvider cacheProvider)
        {
            _dataSerializer = dataSerializer;
            _logger = logger;
            _cacheProvider = cacheProvider;
        }

        public Setting Get<T>(string keyName, int storeId) where T : ISettingGroup
        {
            var groupName = typeof(T).Name;
            var settings = Repository.Where(x => x.Key == keyName);
            if (!string.IsNullOrEmpty(groupName))
                settings = settings.Where(x => x.GroupName == groupName);

            return settings.SelectSingle();
        }

        public void Save<T>(string keyName, string keyValue, int storeId) where T : ISettingGroup
        {
            Save(typeof(T), keyName, keyValue, storeId);
          
        }

        public void Save<T>(T settings, int storeId) where T : ISettingGroup
        {
            Save(typeof(T), settings, storeId);
        }

        public void Save(Type settingType, object settings, int storeId)
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
                Save(settingType, propertyName, value, storeId);
            }
        }

        public void Save(Type settingType, string keyName, string keyValue, int storeId)
        {
            var groupName = settingType.Name;

            //check if setting exist
            var setting = Repository.Where(x => x.GroupName == groupName && x.Key == keyName).SelectSingle();
            if (setting == null)
            {
                setting = new Setting() {
                    GroupName = groupName,
                    Key = keyName,
                    Value = keyValue,
                    StoreId = storeId
                };
                Insert(setting);
            }
            else
            {
                setting.Value = keyValue;
                Update(setting);
            }
        }

        public T GetSettings<T>(int storeId) where T : ISettingGroup
        {
            //create a new settings object
            var settingsObj = Activator.CreateInstance<T>();

            FurnishInstance(settingsObj, storeId);

            return settingsObj;
        }

        public object GetSettings(Type settingType, int storeId)
        {
            if (typeof(IGlobalSettingGroup).IsAssignableFrom(settingType))
                storeId = 0; //no matter what storeId is , if it's global setting, it's 0

            var settingCacheKey = $"{settingType.FullName}-{storeId}";
            return _cacheProvider.Get(settingCacheKey, () =>
            {
                //create a new settings object
                var settingsObj = Activator.CreateInstance(settingType);
                FurnishInstance(settingsObj, storeId);

                return settingsObj;
            }, int.MaxValue);
        }
        public void LoadSettings<T>(T settingsObject, int storeId) where T : ISettingGroup
        {
            FurnishInstance(settingsObject, storeId);
        }

        public void DeleteSettings<T>(int storeId) where T : ISettingGroup
        {
            var groupName = typeof(T).Name;
            Delete(x => x.GroupName == groupName && x.StoreId == storeId);
        }

        private void FurnishInstance(object settingsInstance, int storeId)
        {
            var settingInstanceType = settingsInstance.GetType();
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = settingInstanceType.GetProperties();
            var allSettings = Repository.Where(x => x.GroupName == settingInstanceType.Name && x.StoreId == storeId).Select().ToList();
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
                        //keep default value
                    }
                   
                }
            }
        }
    }
}