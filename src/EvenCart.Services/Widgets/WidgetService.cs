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