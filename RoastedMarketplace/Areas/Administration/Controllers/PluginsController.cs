using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Plugins;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Plugins;
using RoastedMarketplace.Services.Serializers;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    [CapabilityRequired(CapabilitySystemNames.ManagePlugins)]
    public class PluginsController : FoundationAdminController
    {
        private readonly IModelMapper _modelMapper;
        private readonly IPluginAccountant _pluginAccountant;
        private readonly IDataSerializer _dataSerializer;

        public PluginsController(IModelMapper modelMapper, IPluginAccountant pluginAccountant, IDataSerializer dataSerializer)
        {
            _modelMapper = modelMapper;
            _pluginAccountant = pluginAccountant;
            _dataSerializer = dataSerializer;
        }

        [DualGet("", Name = AdminRouteNames.PluginsList)]
        public IActionResult PluginsList()
        {
            var plugins = _pluginAccountant.GetAvailablePlugins();
            var pluginModels = plugins.Select(x => _modelMapper.Map<PluginInfoModel>(x)).ToList();
            return R.Success.WithGridResponse(plugins.Count, 1, pluginModels.Count)
                .With("plugins", () => pluginModels, () => _dataSerializer.Serialize(pluginModels))
                .Result;
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