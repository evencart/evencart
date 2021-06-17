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

using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Models.Settings;
using EvenCart.Areas.Administration.Models.Themes;
using EvenCart.Data.Entity.Settings;
using EvenCart.Genesis.Mvc;
using EvenCart.Services.Taxes;
using Genesis;
using Genesis.Config;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Infrastructure.Security.Attributes;
using Genesis.Infrastructure.Theming;
using Genesis.Modules.Gdpr;
using Genesis.Modules.Localization;
using Genesis.Modules.Navigation;
using Genesis.Modules.Security;
using Genesis.Modules.Settings;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;
using UrlSettings = EvenCart.Data.Entity.Settings.UrlSettings;

namespace EvenCart.Areas.Administration.Controllers
{
    public class SettingsController : GenesisAdminController
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
            { typeof(GdprSettingsModel), typeof(GdprSettings) },
            { typeof(AffiliateSettingsModel), typeof(AffiliateSettings) },
        };


        private readonly ISettingService _settingService;
        private readonly IModelMapper _modelMapper;
        private readonly ICryptographyService _cryptographyService;
        private readonly IThemeProvider _themeProvider;
        public SettingsController(ISettingService settingService, IModelMapper modelMapper, ICryptographyService cryptographyService, IThemeProvider themeProvider)
        {
            _settingService = settingService;
            _modelMapper = modelMapper;
            _cryptographyService = cryptographyService;
            _themeProvider = themeProvider;
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

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "gdpr")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(GdprSettingsModel gdprSettings)
        {
            SaveSetting(gdprSettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "security")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSettings(SecuritySettingsModel securitySettings)
        {
            SaveSetting(securitySettings);
            return R.Success.Result;
        }

        [DualPost("{settingType}", Name = AdminRouteNames.SaveSettings, OnlyApi = true)]
        [FieldRequired("settingType", "affiliate")]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        [ValidateModelState(ModelType = typeof(AffiliateSettingsModel))]
        public IActionResult SaveSettings(AffiliateSettingsModel affiliateSettings)
        {
            SaveSetting(affiliateSettings);
            return R.Success.Result;
        }

        [DualPost("security/shared-key", Name = AdminRouteNames.SaveSharedSecuritySetting, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageSettings)]
        public IActionResult SaveSharedSecurityKey(string sharedKey)
        {
            //we don't actually save the key, rather we save hash of the key
            //generate a key if nothing was provided
            if (sharedKey.IsNullEmptyOrWhiteSpace())
                sharedKey = _cryptographyService.GetRandomPassword(50);

            var cryptedKey = _cryptographyService.GetMd5Hash(sharedKey);
            var securitySettings = D.Resolve<SecuritySettings>();
            securitySettings.SharedVerificationKey = cryptedKey;
            _settingService.Save(securitySettings, CurrentStore.Id);
            return R.Success.With("key", sharedKey).Result;
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
                    settings = D.Resolve<GeneralSettings>();
                    model = _modelMapper.Map<GeneralSettingsModel>(settings);
                    var menuService = D.Resolve<IMenuService>();

                    var menus = menuService.Get(x => true).ToList();
                    var menuSelectList = SelectListHelper.GetSelectItemList(menus, x => x.Id, x => x.Name);
                    result.WithTimezones();
                    result.With("availableMenus", menuSelectList);

                    //get themes
                    var themes = _themeProvider.GetAvailableThemes()
                        .Select(x => new ThemeInfoModel()
                        {
                            DirectoryName = x.DirectoryName,
                            Name = x.Name,
                            ThumbnailUrl = x.ThumbnailUrl,
                            PendingRestart = x.PendingRestart
                        }).ToList();
                    result.With("availableThemes", themes);
                    break;
                case "order":
                    settings = D.Resolve<OrderSettings>();
                    result.WithAvailableOrderStatusTypes();
                    model = _modelMapper.Map<OrderSettingsModel>(settings);
                    break;
                case "user":
                    settings = D.Resolve<UserSettings>();
                    model = _modelMapper.Map<UserSettingsModel>(settings);
                    result.WithRegistrationModes();
                    break;
                case "url":
                    settings = D.Resolve<UrlSettings>();
                    model = _modelMapper.Map<UrlSettingsModel>(settings);
                    break;
                case "media":
                    settings = D.Resolve<MediaSettings>();
                    model = _modelMapper.Map<MediaSettingsModel>(settings);
                    break;
                case "tax":
                    settings = D.Resolve<TaxSettings>();
                    var taxService = D.Resolve<ITaxService>();
                    var taxes = taxService.Get(x => true).ToList();
                    var taxesSelectList = SelectListHelper.GetSelectItemList(taxes, x => x.Id, x => x.Name);
                    model = _modelMapper.Map<TaxSettingsModel>(settings);
                    result.With("availableTaxes", taxesSelectList);
                    break;
                case "email":
                    settings = D.Resolve<EmailSenderSettings>();
                    model = _modelMapper.Map<EmailSettingsModel>(settings);
                    result.WithEmailAccounts();
                    break;
                case "catalog":
                    settings = D.Resolve<CatalogSettings>();
                    model = _modelMapper.Map<CatalogSettingsModel>(settings);
                    result.WithCatalogPaginationTypes();
                    break;
                case "localization":
                    settings = D.Resolve<LocalizationSettings>();
                    model = _modelMapper.Map<LocalizationSettingsModel>(settings);
                    var currencyService = D.Resolve<ICurrencyService>();
                    var currencies = currencyService.Get(x => x.Published).ToList();
                    var currenciesSelectList = SelectListHelper.GetSelectItemListWithAction(currencies, x => x.Id, x => $"{x.Name} ({x.IsoCode})");
                    result.With("availableCurrencies", currenciesSelectList);

                    var languageService = D.Resolve<ILanguageService>();
                    var languages = languageService.Get(x => x.Published).ToList();
                    var languagesSelectList = SelectListHelper.GetSelectItemList(languages, x => x.CultureCode, x => x.Name);
                    result.With("availableLanguages", languagesSelectList);

                    result.WithCatalogPaginationTypes();
                    break;
                case "gdpr":
                    settings = D.Resolve<GdprSettings>();
                    model = _modelMapper.Map<GdprSettingsModel>(settings);
                    var consentGroupService = D.Resolve<IConsentGroupService>();
                    var consentGroups = consentGroupService.Get(x => true).ToList();
                    var groupSelectList = SelectListHelper.GetSelectItemList(consentGroups, x => x.Id, x => x.Name);
                    result.With("availableConsentGroups", groupSelectList);
                    break;
                case "security":
                    settings = D.Resolve<SecuritySettings>();
                    model = _modelMapper.Map<SecuritySettingsModel>(settings);
                    break;
                case "affiliate":
                    settings = D.Resolve<AffiliateSettings>();
                    model = _modelMapper.Map<AffiliateSettingsModel>(settings);
                    break;
            }

            return result.With("settings", model).With("settingType", settingType).Result;
        }

        private void SaveSetting(SettingsModel settingsModel)
        {
            var settingType = settingsModel.GetType();
            if (_settingsMap.TryGetValue(settingType, out var targetType))
            {
                var resolvedSettings = D.Resolve(targetType);
                _modelMapper.MapType(targetType, settingsModel, resolvedSettings);
                _settingService.Save(targetType, resolvedSettings, CurrentStore.Id);
            }
        }
        #endregion
    }
}