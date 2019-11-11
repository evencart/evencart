using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Plugins;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Plugins;
using EvenCart.Infrastructure.Extensions;

namespace EvenCart.Infrastructure.Plugins
{
    public class PluginAccountant : IPluginAccountant
    {
        private readonly PluginSettings _pluginSettings;
        private readonly IPluginInstallerService _pluginInstallerService;
        public PluginAccountant(PluginSettings pluginSettings, IPluginInstallerService pluginInstallerService)
        {
            _pluginSettings = pluginSettings;
            _pluginInstallerService = pluginInstallerService;
        }

        public IList<PluginInfo> GetInstalledPlugins()
        {
            var availablePlugins = GetAvailablePlugins();
            var installedPlugins = _pluginSettings.GetSitePlugins()
                .Where(x => x.Installed)
                .Select(x => x.PluginSystemName)
                .ToList();

            return availablePlugins.Where(x => installedPlugins.Contains(x.SystemName)).ToList();
        }

        public IList<PluginInfo> GetActivePlugins(Type type = null)
        {
            var activePlugins = GetInstalledPlugins().Where(x => x.Active);
            return type != null ? activePlugins.Where(x => type.IsAssignableFrom(x.PluginType)).ToList() : activePlugins.ToList();
        }

        public IList<PluginInfo> GetAvailablePlugins(bool withWidgets = false)
        {
            var availablePlugins = PluginLoader.GetAvailablePlugins(withWidgets);
            var sitePlugins = _pluginSettings.GetSitePlugins();
            foreach (var ap in availablePlugins)
            {
                var sp = sitePlugins.FirstOrDefault(x => x.PluginSystemName == ap.SystemName);
                if (sp != null)
                {
                    ap.Installed = sp.Installed;
                    ap.Active = sp.Active;
                }
            }
            return availablePlugins;
        }

        public void InstallPlugin(PluginInfo pluginInfo)
        {
            _pluginInstallerService.Install(pluginInfo);
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, true, false);
        }

        public void UninstallPlugin(PluginInfo pluginInfo)
        {
            _pluginInstallerService.Uninstall(pluginInfo);
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, false, false);
        }

        public void ActivatePlugin(PluginInfo pluginInfo)
        {
            UpdatePluginActiveStatus(pluginInfo, true);
        }

        public void DeactivatePlugin(PluginInfo pluginInfo)
        {
            UpdatePluginActiveStatus(pluginInfo, false);
        }

        private void UpdatePluginActiveStatus(PluginInfo pluginInfo, bool active)
        {
            _pluginSettings.UpdatePluginStatus(pluginInfo.SystemName, true, active);
        }

        public IList<WidgetInfo> GetAvailableWidgets()
        {
            var plugins = GetAvailablePlugins(true).Where(x => x.Active).ToList();
            var widgetInfos = plugins.Where(x => x.Installed && x.Active).SelectMany(x =>
            {
                return x.Widgets.Select(y => new WidgetInfo() {
                    PluginName = x.Name,
                    PluginSystemName = x.SystemName,
                    WidgetSystemName = y.SystemName,
                    WidgetDisplayName = y.DisplayName,
                    WidgetZones = y.WidgetZones,
                    ConfigurationUrl = y.ConfigurationUrl,
                    HasConfiguration = y.HasConfiguration,
                    SkipDragging = y.SkipDragging,
                    WidgetInstance = y
                });

            }).ToList();
            //todo:move this to separate file
            //get the widgets already part of solution
            var solutionWidgetTypes = TypeFinder.ClassesOfType<IWidget>(restrictToSolutionAssemblies: true);
            var solutionWidgets = solutionWidgetTypes.Select(x => (IWidget) DependencyResolver.Resolve(x)).ToList();
            foreach (var sw in solutionWidgets)
            {
                if (widgetInfos.Any(x => x.WidgetSystemName == sw.SystemName))
                    continue;
                widgetInfos.Add(new WidgetInfo() {
                    PluginName = "EvenCart",
                    PluginSystemName = "EvenCart.InbuiltWidgets",
                    WidgetSystemName = sw.SystemName,
                    WidgetDisplayName = sw.DisplayName,
                    WidgetZones = sw.WidgetZones,
                    ConfigurationUrl = sw.ConfigurationUrl,
                    HasConfiguration = sw.HasConfiguration,
                    SkipDragging = sw.SkipDragging,
                    WidgetInstance = sw
                });
            }
            return widgetInfos;
        }

        public void AddWidget(string widgetName, string pluginSystemName, string zoneName)
        {
            _pluginSettings.AddWidget(widgetName, pluginSystemName, zoneName);
        }

        public void DeleteWidget(string id)
        {
            _pluginSettings.DeleteWidget(id);
        }
    }
}