using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Widgets;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class WidgetController : FoundationAdminController
    {
        private readonly IWidgetService _widgetService;
        private readonly PluginSettings _pluginSettings;
        private readonly IPluginAccountant _pluginAccountant;
        public WidgetController(IWidgetService widgetService, PluginSettings pluginSettings, IPluginAccountant pluginAccountant)
        {
            _widgetService = widgetService;
            _pluginSettings = pluginSettings;
            _pluginAccountant = pluginAccountant;
        }

        [DualGet("configure", Name = AdminRouteNames.GetWidgetSettings)]
        public IActionResult ConfigureWidget(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            //validate if it's a valid widget
            var widgets = _pluginSettings.GetSiteWidgets();
            var widget = widgets.FirstOrDefault(x => x.Id == id);
            if (widget == null)
                return NotFound();

            var widgetInfo = _pluginAccountant.GetAvailableWidgets()
                .FirstOrDefault(x => x.WidgetSystemName == widget.WidgetSystemName &&
                                     x.PluginSystemName == widget.PluginSystemName);

            if (widgetInfo == null)
                return NotFound();
            var settings = _widgetService.LoadWidgetSettings(id, widgetInfo.WidgetInstance.SettingsType);

            var settingsModel = widgetInfo.WidgetInstance.GetViewObject(settings);
            return R.Success.WithView($"{widget.WidgetSystemName}.Settings")
                .With("settings", settingsModel)
                .With("widgetId", id)
                .Result;
        }

        [DualPost("configure", Name = AdminRouteNames.SaveWidgetSettings)]
        public IActionResult ConfigureWidgetSettings(WidgetSettingsModel widgetSettingsModel)
        {
            _widgetService.SaveWidgetSetting(widgetSettingsModel.Id, widgetSettingsModel);
            return ConfigureWidget(widgetSettingsModel.Id);
        }
    }
}