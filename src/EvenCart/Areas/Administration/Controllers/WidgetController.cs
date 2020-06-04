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

using System.Linq;
using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure;
using EvenCart.Services.Widgets;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
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
            _widgetService.SaveWidgetSetting(widgetSettingsModel.Id, widgetSettingsModel, ApplicationEngine.CurrentStore.Id);
            return ConfigureWidget(widgetSettingsModel.Id);
        }
    }
}