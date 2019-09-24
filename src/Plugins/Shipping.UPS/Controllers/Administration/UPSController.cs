using System.Linq;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shipping.UPS.Models;

namespace Shipping.UPS.Controllers.Administration
{
    public class UPSController : FoundationPluginAdminController
    {
        private readonly ISettingService _settingService;
        private readonly UPSSettings _upsSettings;
        public UPSController(ISettingService settingService, UPSSettings upsSettings)
        {
            _settingService = settingService;
            _upsSettings = upsSettings;
        }

        [DualGet("settings", Name = UPSProviderConfig.UPSProviderSettingsRouteName)]
        public IActionResult Settings()
        {
            var upsModel = new SettingsModel()
            {
                ShipperNumber = _upsSettings.ShipperNumber,
                LicenseNumber = _upsSettings.LicenseNumber,
                UserId = _upsSettings.UserId,
                Password = _upsSettings.Password,
                AdditionalFee = _upsSettings.AdditionalFee,
                CustomerClassificationType = _upsSettings.CustomerClassificationType,
                DebugMode = _upsSettings.DebugMode,
                DefaultPackagingType = _upsSettings.DefaultPackagingType,
                PickupType = _upsSettings.PickupType,
                ActiveServices = _upsSettings.ActiveServices
            };
            var pickupTypes = SelectListHelper.GetSelectItemList<PickupType>();
            var packagingTypes = SelectListHelper.GetSelectItemList<PackagingType>();
            var classificationTypes = SelectListHelper.GetSelectItemList<CustomerClassificationType>();
            var availableServices = UPSProviderConfig.AvailableServices.Select(x => new SelectListItem(x.Key, x.Value)).ToList();
            return R.Success.With("settings", upsModel).With("availableUPSPickupTypes", pickupTypes)
                .With("availableUPSPackagingTypes", packagingTypes)
                .With("availableUPSCustomerClassificationTypes", classificationTypes)
                .With("availableUPSServices", availableServices).Result;
        }

        [DualPost("settings", Name = UPSProviderConfig.UPSProviderSettingsRouteName)]
        [ValidateModelState(ModelType = typeof(SettingsModel))]
        public IActionResult Settings(SettingsModel model)
        {
            _upsSettings.CustomerClassificationType = model.CustomerClassificationType;
            _upsSettings.ActiveServices = model.ActiveServices;
            _upsSettings.AdditionalFee = model.AdditionalFee;
            _upsSettings.DebugMode = model.DebugMode;
            _upsSettings.PickupType = model.PickupType;
            _upsSettings.DefaultPackagingType = model.DefaultPackagingType;
            _upsSettings.UserId = model.UserId;
            _upsSettings.Password = model.Password;
            _upsSettings.ShipperNumber = model.ShipperNumber;
            _upsSettings.LicenseNumber = model.LicenseNumber;
            _settingService.Save(_upsSettings);
            return R.Success.Result;
        }
    }
}