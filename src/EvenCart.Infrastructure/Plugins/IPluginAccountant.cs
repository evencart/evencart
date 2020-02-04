using System;
using System.Collections.Generic;
using EvenCart.Core.Plugins;

namespace EvenCart.Infrastructure.Plugins
{
    public interface IPluginAccountant
    {
        IList<PluginInfo> GetInstalledPlugins();

        IList<PluginInfo> GetActivePlugins(Type type = null);

        IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false);

        void InstallPlugin(PluginInfo pluginInfo);

        void UninstallPlugin(PluginInfo pluginInfo);

        void ActivatePlugin(PluginInfo pluginInfo);

        void DeactivatePlugin(PluginInfo pluginInfo);

        IList<WidgetInfo> GetAvailableWidgets();

        string AddWidget(string widgetName, string pluginSystemName, string zoneName);

        void DeleteWidget(string id);

        int GetActiveWidgetCount(string widgetZone);

        bool HandleZipUpload(byte[] fileBytes);
    }
}