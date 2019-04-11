using System;

namespace EvenCart.Services.Widgets
{
    public interface IWidgetService
    {
        T LoadWidgetSettings<T>(string widgetId);

        void SaveWidgetSetting<T>(string widgetId, T settings);

        object LoadWidgetSettings(string widgetId, Type type);
    }
}