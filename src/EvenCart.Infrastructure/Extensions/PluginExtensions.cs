using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Plugins;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Services.Serializers;
using EvenCart.Services.Settings;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Services.Payments;
using EvenCart.Services.Plugins;
using Newtonsoft.Json;

namespace EvenCart.Infrastructure.Extensions
{
    public static class PluginExtensions
    {
        public static IList<PluginStatus> GetSitePlugins(this PluginSettings pluginSettings)
        {
            var sitePlugins = pluginSettings.SitePlugins;
            if (sitePlugins.IsNullEmptyOrWhiteSpace())
                return new List<PluginStatus>();

            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
            return dataSerializer.DeserializeAs<IList<PluginStatus>>(sitePlugins);
        }

        public static void SetSitePlugins(this PluginSettings pluginSettings, IList<PluginStatus> pluginStatuses, bool save = false)
        {
            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
            var sitePlugins = dataSerializer.Serialize(pluginStatuses);
            pluginSettings.SitePlugins = sitePlugins;
            if (save)
            {
                var settingService = DependencyResolver.Resolve<ISettingService>();
                settingService.Save(pluginSettings, 0);
            }
        }

        public static void UpdatePluginStatus(this PluginSettings pluginSettings, string systemName, bool installed,
            bool active)
        {
            var pluginStatus = pluginSettings.GetSitePlugins();
            var ps = pluginStatus.FirstOrDefault(x => x.PluginSystemName == systemName);
            if (ps == null)
            {
                ps = new PluginStatus()
                {
                    PluginSystemName = systemName
                };
                pluginStatus.Add(ps);
            }
            ps.Installed = installed;
            var storeId = ApplicationEngine.CurrentStore.Id;
            if (active)
            {
                if (!ps.ActiveStoreIds.Contains(storeId))
                    ps.ActiveStoreIds.Add(storeId);
            }
            else
            {
                ps.ActiveStoreIds.Remove(storeId);
            }
            pluginSettings.SetSitePlugins(pluginStatus, true);
        }

        public static IList<WidgetStatus> GetSiteWidgets(this PluginSettings pluginSettings, bool activeOnly = false)
        {
            var siteWidgets = pluginSettings.SiteWidgets;
            if (siteWidgets.IsNullEmptyOrWhiteSpace())
                return new List<WidgetStatus>();
            var storeId = ApplicationEngine.CurrentStore.Id;
            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
            var widgets = dataSerializer.DeserializeAs<IList<WidgetStatus>>(siteWidgets).Where(x => x.StoreId == storeId);
            if (activeOnly)
            {
                var sitePlugins = pluginSettings.GetSitePlugins().Where(x => x.ActiveStoreIds.Contains(storeId)).Select(x => x.PluginSystemName);
                widgets = widgets.Where(x => x.PluginSystemName == ApplicationConfig.InbuiltWidgetPluginName || sitePlugins.Contains(x.PluginSystemName)).ToList();
            }
            return widgets.OrderBy(x => x.DisplayOrder).ToList();
        }

        public static string AddWidget(this PluginSettings pluginSettings, string widgetName, string pluginSystemName, string zoneName)
        {
            var siteWidgets = pluginSettings.GetSiteWidgets();
            //find widget count in zone
            var zoneWidgetCount = siteWidgets.Count(x => x.ZoneName == zoneName);
            var widgetId = Guid.NewGuid().ToString();
            siteWidgets.Add(new WidgetStatus()
            {
                WidgetSystemName = widgetName,
                PluginSystemName = pluginSystemName,
                ZoneName = zoneName,
                DisplayOrder = zoneWidgetCount,
                Id = widgetId,
                StoreId = ApplicationEngine.CurrentStore.Id
            });

            pluginSettings.SaveWidgets(siteWidgets, true);
            return widgetId;
        }

        public static void SaveWidgets(this PluginSettings pluginSettings, IList<WidgetStatus> widgets, bool save = false)
        {
            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
            var sitePlugins = dataSerializer.Serialize(widgets);
            pluginSettings.SiteWidgets = sitePlugins;
            if (save)
            {
                var settingService = DependencyResolver.Resolve<ISettingService>();
                settingService.Save(pluginSettings, ApplicationEngine.CurrentStore.Id);
            }
        }

        public static bool Supports(this IPaymentHandlerPlugin plugin, PaymentOperation operation)
        {
            return plugin.SupportedOperations.Contains(operation);
        }

        public static void DeleteWidget(this PluginSettings pluginSettings, string id)
        {
            var widgets = pluginSettings.GetSiteWidgets();
            var widget = widgets.FirstOrDefault(x => x.Id == id);
            if (widget != null)
                widgets.Remove(widget);
            pluginSettings.SaveWidgets(widgets, true);
        }

        /// <summary>
        /// Updates the install/active property values in plugin info. Should be called 
        /// </summary>
        internal static IList<PluginInfo> UpdateInstallStatus(this IList<PluginInfo> pluginInfos)
        {
            //this is called during app startup when database has been initialized and services are not available yet, therefore we'll manually fetch the plugin install status
            //todo: find if there is a better way than this
            //if (!DatabaseManager.IsDatabaseInstalled())
            //    return pluginInfos;
            var setting = FoundationEntityService<Setting>.Get(x =>
                x.GroupName == nameof(PluginSettings) && x.Key == nameof(PluginSettings.SitePlugins)).FirstOrDefault();
            if (setting == null || setting.Value.IsNullEmptyOrWhiteSpace())
                return pluginInfos;
            var pluginStatus = JsonConvert.DeserializeObject<IList<PluginStatus>>(setting.Value);
            foreach (var pluginInfo in pluginInfos)
            {
                var ps = pluginStatus.FirstOrDefault(x => x.PluginSystemName == pluginInfo.SystemName);
                pluginInfo.Installed = ps?.Installed ?? false;
                pluginInfo.ActiveStoreIds = ps?.ActiveStoreIds ?? new List<int>();
            }

            return pluginInfos;
        }
    }
}