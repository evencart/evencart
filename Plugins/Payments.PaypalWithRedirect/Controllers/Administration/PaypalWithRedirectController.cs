using Microsoft.AspNetCore.Mvc;
using Payments.PaypalWithRedirect.Models;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Settings;

namespace Payments.PaypalWithRedirect.Controllers.Administration
{
    public class PaypalWithRedirectController : FoundationAdminController
    {
        private readonly PaypalWithRedirectSettings _paypalWithRedirectSettings;
        private readonly ISettingService _settingService;
        public PaypalWithRedirectController(PaypalWithRedirectSettings paypalWithRedirectSettings, ISettingService settingService)
        {
            _paypalWithRedirectSettings = paypalWithRedirectSettings;
            _settingService = settingService;
        }
        [HttpGet("settings", Name = PaypalConfig.PaypalWithRedirectSettingsRouteName)]
        public IActionResult Settings()
        {
            var settingsModel = new SettingsModel()
            {
                ClientId = _paypalWithRedirectSettings.ClientId,
                ClientSecret = _paypalWithRedirectSettings.ClientSecret,
                EnableSandbox = _paypalWithRedirectSettings.EnableSandbox
            };
            return R.Success.With("settings", settingsModel).Result;
        }

        [DualPost("settings", Name = PaypalConfig.PaypalWithRedirectSettingsRouteName, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SettingsModel))]
        public IActionResult SettingsSave(SettingsModel settingsModel)
        {
            _paypalWithRedirectSettings.ClientId = settingsModel.ClientId;
            _paypalWithRedirectSettings.ClientSecret = settingsModel.ClientSecret;
            _paypalWithRedirectSettings.EnableSandbox = settingsModel.EnableSandbox;
            _settingService.Save(_paypalWithRedirectSettings);
            return R.Success.Result;
        }
    }
}