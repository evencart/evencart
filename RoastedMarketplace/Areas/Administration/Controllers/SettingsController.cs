using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Models.Settings;
using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.Helpers;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Services.Settings;
using RoastedMarketplace.Services.Taxes;

namespace RoastedMarketplace.Areas.Administration.Controllers
{
    public class SettingsController : FoundationAdminController
    {
        private readonly Dictionary<Type, Type> _settingsMap = new Dictionary<Type, Type>()
        {
            { typeof(GeneralSettingsModel), typeof(GeneralSettings) },
            { typeof(MediaSettingsModel), typeof(MediaSettings) },
            { typeof(CatalogSettingsModel), typeof(CatalogSettings) },
            { typeof(UrlSettingsModel), typeof(UrlSettings) },
            { typeof(EmailSettingsModel), typeof(EmailSenderSettings) },
            { typeof(OrderSettingsModel), typeof(OrderSettings) },
            { typeof(SecuritySettingsModel), typeof(SecuritySettings) },
            { typeof(SystemSettingsModel), typeof(SystemSettings) },
            { typeof(TaxSettingsModel), typeof(TaxSettings) },
            { typeof(UserSettingsModel), typeof(UserSettings) },
        };


        private readonly ISettingService _settingService;
        private readonly IModelMapper _modelMapper;
        public SettingsController(ISettingService settingService, IModelMapper modelMapper)
        {
            _settingService = settingService;
            _modelMapper = modelMapper;
        }

        [DualGet("{settingType}", Name = AdminRouteNames.GetSettings)]
        public IActionResult Index(string settingType = "general")
        {
            return GetSettingResult(settingType);
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "general")]
        public IActionResult SaveSettings(GeneralSettingsModel generalSettings)
        {
            SaveSetting(generalSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "order")]
        public IActionResult SaveSettings(OrderSettingsModel orderSettings)
        {
            SaveSetting(orderSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "user")]
        public IActionResult SaveSettings(UserSettingsModel userSettings)
        {
            SaveSetting(userSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "tax")]
        public IActionResult SaveSettings(TaxSettingsModel taxSettings)
        {
            SaveSetting(taxSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "url")]
        public IActionResult SaveSettings(UrlSettingsModel urlSettings)
        {
            SaveSetting(urlSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "media")]
        public IActionResult SaveSettings(MediaSettingsModel mediaSettings)
        {
            SaveSetting(mediaSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "catalog")]
        public IActionResult SaveSettings(CatalogSettingsModel catalogSettings)
        {
            SaveSetting(catalogSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "email")]
        public IActionResult SaveSettings(EmailSettingsModel emailSettings)
        {
            SaveSetting(emailSettings);
            return R.Success.Result;
        }

        #region Helpers
        private IActionResult GetSettingResult(string settingType)
        {
            SettingsModel model = null;
            ISettingGroup settings = null;
            var result = R.Success;
            switch (settingType)
            {
                case "general":
                    settings = DependencyResolver.Resolve<GeneralSettings>();
                    model = _modelMapper.Map<GeneralSettingsModel>(settings);
                    result.WithTimezones();
                    break;
                case "order":
                    settings = DependencyResolver.Resolve<OrderSettings>();
                    model = _modelMapper.Map<OrderSettingsModel>(settings);
                    break;
                case "user":
                    settings = DependencyResolver.Resolve<UserSettings>();
                    model = _modelMapper.Map<UserSettingsModel>(settings);
                    result.WithRegistrationModes();
                    break;
                case "url":
                    settings = DependencyResolver.Resolve<UrlSettings>();
                    model = _modelMapper.Map<UrlSettingsModel>(settings);
                    break;
                case "media":
                    settings = DependencyResolver.Resolve<MediaSettings>();
                    model = _modelMapper.Map<MediaSettingsModel>(settings);
                    break;
                case "tax":
                    settings = DependencyResolver.Resolve<TaxSettings>();
                    var taxService = DependencyResolver.Resolve<ITaxService>();
                    var taxes = taxService.Get(x => true).ToList();
                    var taxesSelectList = SelectListHelper.GetSelectItemList(taxes, x => x.Id, x => x.Name);
                    model = _modelMapper.Map<TaxSettingsModel>(settings);
                    result.With("availableTaxes", taxesSelectList);
                    break;
                case "email":
                    settings = DependencyResolver.Resolve<EmailSenderSettings>();
                    model = _modelMapper.Map<EmailSettingsModel>(settings);
                    result.WithEmailAccounts();
                    break;
                case "catalog":
                    settings = DependencyResolver.Resolve<CatalogSettings>();
                    model = _modelMapper.Map<CatalogSettingsModel>(settings);
                    result.WithCatalogPaginationTypes();
                    break;
            }

            return result.With("settings", model).With("settingType", settingType).Result;
        }

        private void SaveSetting(SettingsModel settingsModel)
        {
            var settingType = settingsModel.GetType();
            if (_settingsMap.TryGetValue(settingType, out Type targetType))
            {
                var resolvedSettings = DependencyResolver.Resolve(targetType);
                _modelMapper.MapType(targetType, settingsModel, resolvedSettings);
                _settingService.Save(targetType, resolvedSettings, true);
            }
        }
        #endregion
    }
}