using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Shipping.Shippo.Models;

namespace Shipping.Shippo.Controllers.Administration
{
    public class ShippoController : FoundationPluginAdminController
    {
        private readonly ISettingService _settingService;
        private readonly Settings _ShippoSettings;
        public ShippoController(ISettingService settingService, Settings ShippoSettings)
        {
            _settingService = settingService;
            _ShippoSettings = ShippoSettings;
        }

        [DualGet("settings", Name = ProviderConfig.ShippoProviderSettingsRouteName)]
        public IActionResult Settings()
        {
            var ShippoModel = new SettingsModel()
            {
                LiveApiKey = _ShippoSettings.LiveApiKey,
                TestApiKey = _ShippoSettings.TestApiKey,
                DebugMode = _ShippoSettings.DebugMode,
            };
            return R.Success.With("settings", ShippoModel).Result;
        }

        [DualPost("settings", Name = ProviderConfig.ShippoProviderSettingsRouteName)]
        [ValidateModelState(ModelType = typeof(SettingsModel))]
        public IActionResult Settings(SettingsModel model)
        {
            _ShippoSettings.DebugMode = model.DebugMode;
            _ShippoSettings.LiveApiKey = model.LiveApiKey;
            _ShippoSettings.TestApiKey = model.TestApiKey;
            _settingService.Save(_ShippoSettings);
            return R.Success.Result;
        }
    }
}