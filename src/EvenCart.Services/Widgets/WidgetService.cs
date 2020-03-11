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
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Serializers;
using EvenCart.Services.Settings;

namespace EvenCart.Services.Widgets
{
    public class WidgetService : IWidgetService
    {
        private const string WidgetSettingKey = "widget_{0}";
        private readonly ISettingService _settingService;
        private readonly IDataSerializer _dataSerializer;
        public WidgetService(ISettingService settingService, IDataSerializer dataSerializer)
        {
            _settingService = settingService;
            _dataSerializer = dataSerializer;
        }

        public T LoadWidgetSettings<T>(string widgetId)
        {
            var widgetSettingName = string.Format(WidgetSettingKey, widgetId);
            var setting = _settingService.FirstOrDefault(x => x.Key == widgetSettingName);
            if (setting == null)
                return Activator.CreateInstance<T>();

            return _dataSerializer.DeserializeAs<T>(setting.Value);
        }

        public void SaveWidgetSetting<T>(string widgetId, T settings)
        {
            var widgetSettingName = string.Format(WidgetSettingKey, widgetId);
            var setting = _settingService.FirstOrDefault(x => x.Key == widgetSettingName);
            if (setting == null)
                setting = new Setting()
                {
                    Key = widgetSettingName,
                    GroupName = "WidgetSettings"
                };
            setting.Value = _dataSerializer.Serialize(settings);

            _settingService.InsertOrUpdate(setting);
        }

        public object LoadWidgetSettings(string widgetId, Type type)
        {
            var widgetSettingName = string.Format(WidgetSettingKey, widgetId);
            var setting = _settingService.FirstOrDefault(x => x.Key == widgetSettingName);
            if (setting == null)
                return Activator.CreateInstance(type);
            return _dataSerializer.Deserialize(setting.Value, type);
        }
    }
}