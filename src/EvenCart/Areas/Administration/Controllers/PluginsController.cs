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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using EvenCart.Areas.Administration.Extensions;
using EvenCart.Areas.Administration.Models.Plugins;
using EvenCart.Core.Data;
using EvenCart.Data.Constants;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.HttpServices;
using EvenCart.Services.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    [CapabilityRequired(CapabilitySystemNames.ManagePlugins)]
    public class PluginsController : FoundationAdminController
    {
        private const string MarketPluginUrl = "https://evencart.co/api/market";
        private readonly IModelMapper _modelMapper;
        private readonly IPluginAccountant _pluginAccountant;
        private readonly IDataSerializer _dataSerializer;
        private readonly PluginSettings _pluginSettings;
        private readonly IRequestProvider _requestProvider;
        public PluginsController(IModelMapper modelMapper, IPluginAccountant pluginAccountant, IDataSerializer dataSerializer, PluginSettings pluginSettings, IRequestProvider requestProvider)
        {
            _modelMapper = modelMapper;
            _pluginAccountant = pluginAccountant;
            _dataSerializer = dataSerializer;
            _pluginSettings = pluginSettings;
            _requestProvider = requestProvider;
        }

        [DualGet("", Name = AdminRouteNames.PluginsList)]
        public IActionResult PluginsList()
        {
            var plugins = _pluginAccountant.GetAvailablePlugins();
            var pluginModels = plugins.Select(x =>
            {
                var pluginInfo = _modelMapper.Map<PluginInfoModel>(x);
                pluginInfo.Active = x.ActiveStoreIds.Contains(CurrentStore.Id);
                pluginInfo.ConfigurationUrl = x.ConfigurationUrl;
                return pluginInfo;
            }).OrderBy(x => x.Name).ToList();
            return R.Success.WithGridResponse(plugins.Count, 1, pluginModels.Count)
                .With("plugins", pluginModels)
                .Result;
        }

        [DualGet("widgets", Name = AdminRouteNames.WidgetsList)]
        public IActionResult WidgetsList()
        {
            //find the active widgets
            var availableWidgets = _pluginAccountant.GetAvailableWidgets();
            var widgetModels = availableWidgets.Where(x => !x.SkipDragging).Select(x => new WidgetModel() {
                PluginName = x.PluginName,
                PluginSystemName = x.PluginSystemName,
                WidgetName = x.WidgetDisplayName,
                WidgetSystemName = x.WidgetSystemName,
                WidgetZones = x.WidgetZones?.OrderBy(y => y).ToList()
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
            }).OrderBy(x => x.Name)
                .ToList();

            return R.Success
                .With("availableWidgets", widgetModels)
                .With("activeWidgets", activeWidgets)
                .With("zones", zones)
                .WithParams(null)
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
           _pluginSettings.DeleteWidget(id);
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

            if (pluginInfoModel.Active && !plugin.ActiveStoreIds.Contains(CurrentStore.Id))
            {
                _pluginAccountant.ActivatePlugin(plugin);
            }
            else if (!pluginInfoModel.Active && plugin.ActiveStoreIds.Contains(CurrentStore.Id))
            {
                _pluginAccountant.DeactivatePlugin(plugin);
            }
            return R.Success.Result;
        }

        [DualPost("dbupgrade", Name = AdminRouteNames.UpgradeDbPlugin, OnlyApi = true)]
        public IActionResult UpgradePlugin(string systemName)
        {
            if (systemName.IsNullEmptyOrWhiteSpace())
                return R.Fail.Result;
            var pluginInfo = _pluginAccountant.GetAvailablePlugins(true)
                .FirstOrDefault(x => x.Dirty && x.Installed && x.SystemName == systemName);
            if (pluginInfo == null)
                return NotFound();

            DatabaseManager.UpgradeDatabase(systemName);
            //it's not longer dirty now
            pluginInfo.Dirty = false;
            return R.Success.Result;
        }

        /// <summary>
        /// The list paginated of plugins in EvenCart marketplace
        /// </summary>
        /// <param name="searchModel">The <see cref="PluginsSearchModel">search</see> object for plugins</param>
        /// <response code="200">A list of <see cref="PluginInfoModel">plugin</see> objects as 'plugins'</response>
        [DualGet("market", Name = AdminRouteNames.MarketPluginsList)]
        [CapabilityRequired(CapabilitySystemNames.ManagePlugins)]
        [ValidateModelState(ModelType = typeof(PluginsSearchModel))]
        public async Task<IActionResult> MarketPluginsList(PluginsSearchModel searchModel)
        {
            var marketPluginInfos = await _requestProvider.GetAsync<MarketPluginInfosModel>(MarketPluginUrl,
                new NameValueCollection()
                {
                    {"current", searchModel.Current.ToString()},
                    {"rowCount", searchModel.RowCount.ToString()},
                    {"searchPhrase", searchModel.SearchPhrase}
                });

            if (!marketPluginInfos.Success)
                return R.Fail.Result;
            return R.Success
                .WithGridResponse(marketPluginInfos.Total, marketPluginInfos.Current, marketPluginInfos.RowCount)
                .With("plugins", marketPluginInfos.Plugins)
                .Result;
        }

        [DualGet("payment-methods", Name = AdminRouteNames.PaymentMethodsList, OnlyApi = true)]
        public IActionResult PaymentMethodsList()
        {
            var paymentHandlers = _pluginAccountant.GetActivePlugins(typeof(IPaymentHandlerPlugin));
            var models = paymentHandlers.Select(x => new {Id = x.SystemName, x.Name}).ToList();
            return R.Success.With("paymentMethods", models).WithGridResponse(paymentHandlers.Count, 1, paymentHandlers.Count).Result;
        }

        [DualGet("shipping-methods", Name = AdminRouteNames.ShippingMethodsList, OnlyApi = true)]
        public IActionResult ShippingMethodsList()
        {
            var shipmentHandlers = _pluginAccountant.GetActivePlugins(typeof(IShipmentHandlerPlugin));
            var models = shipmentHandlers.Select(x => new { Id = x.SystemName, x.Name }).ToList();
            return R.Success.With("shippingMethods", models).WithGridResponse(shipmentHandlers.Count, 1, shipmentHandlers.Count).Result;
        }

        [DualPost("package-upload", Name = AdminRouteNames.UploadPackage, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManagePlugins)]
        [ValidateModelState(ModelType = typeof(UploadPackageModel))]
        public async Task<IActionResult> UploadArchive(UploadPackageModel packageModel)
        {
            var fileBytes = await packageModel.MediaFile.GetBytesAsync();
            var success = _pluginAccountant.HandleZipUpload(fileBytes);
            return success ? R.Success.Result : R.Fail.With("error", T("An error occurred while handling package. Please check log for details.")).Result;
        }
    }
}