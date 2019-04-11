using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Plugins;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Serializers;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    [CapabilityRequired(CapabilitySystemNames.ManagePlugins)]
    public class PluginsController : FoundationAdminController
    {
        private readonly IModelMapper _modelMapper;
        private readonly IPluginAccountant _pluginAccountant;
        private readonly IDataSerializer _dataSerializer;
        private readonly PluginSettings _pluginSettings;
        public PluginsController(IModelMapper modelMapper, IPluginAccountant pluginAccountant, IDataSerializer dataSerializer, PluginSettings pluginSettings)
        {
            _modelMapper = modelMapper;
            _pluginAccountant = pluginAccountant;
            _dataSerializer = dataSerializer;
            _pluginSettings = pluginSettings;
        }

        [DualGet("", Name = AdminRouteNames.PluginsList)]
        public IActionResult PluginsList()
        {
            var plugins = _pluginAccountant.GetAvailablePlugins();
            var pluginModels = plugins.Select(x =>
            {
                var pluginInfo = _modelMapper.Map<PluginInfoModel>(x);
                pluginInfo.ConfigurationUrl = x.ConfigurationUrl;
                return pluginInfo;
            }).ToList();
            return R.Success.WithGridResponse(plugins.Count, 1, pluginModels.Count)
                .With("plugins", () => pluginModels, () => _dataSerializer.Serialize(pluginModels))
                .Result;
        }

        [DualGet("widgets", Name = AdminRouteNames.WidgetsList)]
        public IActionResult WidgetsList()
        {
            //find the active widgets
            var availableWidgets = _pluginAccountant.GetAvailableWidgets();
            var widgetModels = availableWidgets.Select(x => new WidgetModel() {
                PluginName = x.PluginName,
                PluginSystemName = x.PluginSystemName,
                WidgetName = x.WidgetDisplayName,
                WidgetSystemName = x.WidgetSystemName,
                WidgetZones = x.WidgetZones
            });
            widgetModels = widgetModels.OrderBy(x => x.PluginName).ToList();
            var availableWidgetNames = widgetModels.Select(x => x.WidgetSystemName);
            var activeWidgets = _pluginSettings.GetSiteWidgets()
                .Where(x => availableWidgetNames.Contains(x.WidgetSystemName))
                .GroupBy(x => x.ZoneName)
                .Select(x => new WidgetInfoModel() {
                    ZoneName = x.Key,
                    Widgets = x.Select(y =>
                        {
                            var widget = availableWidgets
                                .FirstOrDefault(z => z.PluginSystemName == y.PluginSystemName && z.WidgetSystemName == y.WidgetSystemName);
                            var widgetModel = new WidgetModel() {
                                PluginSystemName = y.PluginSystemName,
                                WidgetSystemName = y.WidgetSystemName,
                                PluginName = widget?.PluginName,
                                WidgetName = widget?.WidgetDisplayName,
                                HasConfiguration = widget?.HasConfiguration ?? false,
                                Id = y.Id
                            };
                            if (widgetModel.HasConfiguration)
                            {
                                widgetModel.ConfigurationUrl = widget?.ConfigurationUrl ?? ApplicationEngine.RouteUrl(AdminRouteNames.GetWidgetSettings, new {id = y.Id});
                            }
                            return widgetModel;
                        })
                        .ToList()
                })
                .ToList();

            //available zones
            var zones = ApplicationEngine.ActiveTheme.WidgetZones.Select(x => new ZoneModel() {
                Name = x.Value,
                SystemName = x.Key
            })
                .ToList();

            return R.Success
                .With("availableWidgets", widgetModels)
                .With("activeWidgets", activeWidgets)
                .With("zones", zones)
                .Result;
        }

        [DualPost("widgets", Name = AdminRouteNames.SaveWidget, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(WidgetModel))]
        public IActionResult SaveWidget(WidgetModel widgetModel)
        {
            var widgets = _pluginAccountant.GetAvailableWidgets();
            if (!widgets.Any(x => x.PluginSystemName == widgetModel.PluginSystemName &&
                                  x.WidgetSystemName == widgetModel.WidgetSystemName))
                return R.Fail.With("error", T("Widget not found")).Result;

            if (!ApplicationEngine.ActiveTheme.WidgetZones.ContainsKey(widgetModel.ZoneName))
                return R.Fail.With("error", T("Zone not found")).Result;

            _pluginSettings.AddWidget(widgetModel.WidgetSystemName, widgetModel.PluginSystemName, widgetModel.ZoneName);
            return R.Success.Result;
        }

        [DualPost("widgets/displayorder", Name = AdminRouteNames.UpdateWidgetsDisplayOrder, OnlyApi = true)]
        public IActionResult UpdateWidgets(IList<WidgetModel> widgetModels)
        {
            if (widgetModels == null || !widgetModels.Any())
                return R.Fail.Result;

            var widgets = _pluginSettings.GetSiteWidgets();
            foreach (var widget in widgets)
            {
                var wm = widgetModels.FirstOrDefault(x => x.WidgetSystemName == widget.WidgetSystemName &&
                                                          x.PluginSystemName == widget.PluginSystemName && 
                                                          x.Id == widget.Id );
                if (wm == null)
                    continue;
                widget.DisplayOrder = wm.DisplayOrder;
            }
            //save the widgets
            _pluginSettings.SaveWidgets(widgets, true);
            return R.Success.Result;
        }

        [DualPost("widgets/delete", Name = AdminRouteNames.DeleteWidget, OnlyApi = true)]
        public IActionResult DeleteWidget(string id)
        {
            var widgets = _pluginSettings.GetSiteWidgets();
            var widget = widgets.FirstOrDefault(x => x.Id == id);
            if (widget != null)
                widgets.Remove(widget);
            _pluginSettings.SaveWidgets(widgets, true);
            return R.Success.Result;
        }

        [DualPost("", Name = AdminRouteNames.UpdatePluginStatus, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(PluginInfoModel))]
        public IActionResult UpdatePluginStatus(PluginInfoModel pluginInfoModel)
        {
            var plugin = _pluginAccountant.GetAvailablePlugins().FirstOrDefault(x => x.SystemName == pluginInfoModel.SystemName);
            if (plugin == null)
                return NotFound();
            if (pluginInfoModel.Installed && !plugin.Installed)
            {
                _pluginAccountant.InstallPlugin(plugin);
            }
            else if (!pluginInfoModel.Installed && plugin.Installed)
            {
                _pluginAccountant.UninstallPlugin(plugin);
            }

            if (pluginInfoModel.Active && !plugin.Active)
            {
                _pluginAccountant.ActivatePlugin(plugin);
            }
            else if (!pluginInfoModel.Active && plugin.Active)
            {
                _pluginAccountant.DeactivatePlugin(plugin);
            }
            return R.Success.Result;
        }
    }
}