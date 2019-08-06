using EvenCart.Services.Settings;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;
using Payments.Stripe.Models;

namespace Payments.Stripe.Controllers.Administration
{
    public class StripeController : FoundationAdminController
    {
        private readonly StripeSettings _StripeSettings;
        private readonly ISettingService _settingService;
        public StripeController(StripeSettings StripeSettings, ISettingService settingService)
        {
            _StripeSettings = StripeSettings;
            _settingService = settingService;
        }
        [HttpGet("settings", Name = StripeConfig.StripeSettingsRouteName)]
        public IActionResult Settings()
        {
            var settingsModel = new SettingsModel()
            {
                ClientId = _StripeSettings.ClientId,
                ClientSecret = _StripeSettings.ClientSecret,
                EnableSandbox = _StripeSettings.EnableTestMode
            };
            return R.Success.With("settings", settingsModel).Result;
        }

        [DualPost("settings", Name = StripeConfig.StripeSettingsRouteName, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SettingsModel))]
        public IActionResult SettingsSave(SettingsModel settingsModel)
        {
            _StripeSettings.ClientId = settingsModel.ClientId;
            _StripeSettings.ClientSecret = settingsModel.ClientSecret;
            _StripeSettings.EnableTestMode = settingsModel.EnableSandbox;
            _settingService.Save(_StripeSettings);
            return R.Success.Result;
        }
    }
}