using System;
using System.Collections.Generic;
using RoastedMarketplace.Core.Plugins;

namespace RoastedMarketplace.Infrastructure.Plugins
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
    }
}