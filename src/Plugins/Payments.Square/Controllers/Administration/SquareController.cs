using EvenCart.Services.Settings;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Mvc;
using Payments.Square.Helpers;
using Payments.Square.Models;

namespace Payments.Square.Controllers.Administration
{
    public class SquareController : FoundationPluginAdminController
    {
        private readonly SquareSettings _squareSettings;
        private readonly ISettingService _settingService;
        public SquareController(SquareSettings squareSettings, ISettingService settingService)
        {
            _squareSettings = squareSettings;
            _settingService = settingService;
        }
        [HttpGet("settings", Name = SquareConfig.SquareSettingsRouteName)]
        public IActionResult Settings()
        {
            var settingsModel = new SettingsModel()
            {
                Description = _squareSettings.Description,
                SandboxApplicationId = _squareSettings.SandboxApplicationId,
                SandboxAccessToken = _squareSettings.SandboxAccessToken,
                AdditionalFee = _squareSettings.AdditionalFee,
                AuthorizeOnly = _squareSettings.AuthorizeOnly,
                EnableSandbox = _squareSettings.EnableSandbox,
                ApplicationId = _squareSettings.ApplicationId,
                AccessToken = _squareSettings.AccessToken,
                UsePercentageForAdditionalFee = _squareSettings.UsePercentageForAdditionalFee,
                LocationId = _squareSettings.LocationId
            };
            return R.Success.With("settings", settingsModel).Result;
        }

        [DualPost("settings", Name = SquareConfig.SquareSettingsRouteName, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SettingsModel))]
        public IActionResult SettingsSave(SettingsModel settingsModel)
        {
            _squareSettings.Description = settingsModel.Description;
            _squareSettings.SandboxApplicationId = settingsModel.SandboxApplicationId;
            _squareSettings.SandboxAccessToken = settingsModel.SandboxAccessToken;
            _squareSettings.AdditionalFee = settingsModel.AdditionalFee;
            _squareSettings.AuthorizeOnly = settingsModel.AuthorizeOnly;
            _squareSettings.EnableSandbox = settingsModel.EnableSandbox;
            _squareSettings.ApplicationId = settingsModel.ApplicationId;
            _squareSettings.AccessToken = settingsModel.AccessToken;
            _squareSettings.LocationId = settingsModel.LocationId;
            _squareSettings.UsePercentageForAdditionalFee = settingsModel.UsePercentageForAdditionalFee;
            _settingService.Save(_squareSettings);
            //reset settings
            SquareHelper.InitSquare(_squareSettings);
            return R.Success.Result;
        }
    }
}