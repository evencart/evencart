using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Settings;
using EvenCart.Core.Config;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Cultures;
using EvenCart.Services.Navigation;
using EvenCart.Services.Settings;
using EvenCart.Services.Taxes;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
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
            { typeof(LocalizationSettingsModel), typeof(LocalizationSettings) },
        };


        private readonly ISettingService _settingService;
        private readonly IModelMapper _modelMapper;
        public SettingsController(ISettingService settingService, IModelMapper modelMapper)
        {
            _settingService = settingService;
            _modelMapper = modelMapper;
        }

        [DualGet("{settingType}", Name = AdminRouteNames.GetSettings)]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult Index(string settingType = "general")
        {
            return GetSettingResult(settingType);
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "general")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(GeneralSettingsModel generalSettings)
        {
            SaveSetting(generalSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "order")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(OrderSettingsModel orderSettings)
        {
            SaveSetting(orderSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "user")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(UserSettingsModel userSettings)
        {
            SaveSetting(userSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "tax")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(TaxSettingsModel taxSettings)
        {
            SaveSetting(taxSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "url")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(UrlSettingsModel urlSettings)
        {
            SaveSetting(urlSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "media")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(MediaSettingsModel mediaSettings)
        {
            SaveSetting(mediaSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "catalog")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(CatalogSettingsModel catalogSettings)
        {
            SaveSetting(catalogSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "email")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(EmailSettingsModel emailSettings)
        {
            SaveSetting(emailSettings);
            return R.Success.Result;
        }
        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "localization")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(LocalizationSettingsModel localizationSettings)
        {
            SaveSetting(localizationSettings);
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
                    var menuService = DependencyResolver.Resolve<IMenuService>();

                    var menus = menuService.Get(x => true).ToList();
                    var menuSelectList = SelectListHelper.GetSelectItemList(menus, x => x.Id, x => x.Name);
                    result.WithTimezones();
                    result.With("availableMenus", menuSelectList);
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
                case "localization":
                    settings = DependencyResolver.Resolve<LocalizationSettings>();
                    model = _modelMapper.Map<LocalizationSettingsModel>(settings);
                    var currencyService = DependencyResolver.Resolve<ICurrencyService>();
                    var currencies = currencyService.Get(x => x.Published).ToList();
                    var currenciesSelectList = SelectListHelper.GetSelectItemListWithAction(currencies, x => x.Id, x => $"{x.Name} ({x.IsoCode})");
                    result.With("availableCurrencies", currenciesSelectList);

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
                _settingService.Save(targetType, resolvedSettings);
            }
        }
        #endregion
    }
}