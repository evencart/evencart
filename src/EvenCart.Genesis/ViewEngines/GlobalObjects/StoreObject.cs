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
using System.Linq;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Products;
using Genesis.Modules.Settings;
using Genesis.Infrastructure;
using Genesis.Extensions;
using Genesis.Helpers;
using Genesis.MediaServices;
using Genesis.Modules.Gdpr;
using Genesis.Modules.Localization;
using Genesis.Modules.Users;
using Genesis.Modules.Web;
using Genesis.Routing;
using Genesis.ViewEngines.GlobalObjects.Implementations;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.Extensions;
using UrlSettings = Genesis.Modules.Settings.UrlSettings;

namespace Genesis.ViewEngines.GlobalObjects
{
    public class StoreObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = D.Resolve<GeneralSettings>();
            var catalogSettings = D.Resolve<CatalogSettings>();
            var orderSettings = D.Resolve<OrderSettings>();
            var urlSettings = D.Resolve<UrlSettings>();
            var gdprSettings = D.Resolve<GdprSettings>();
            var localizationSettings = D.Resolve<LocalizationSettings>();
            var securitySettings = D.Resolve<SecuritySettings>();
            var vendorSettings = D.Resolve<VendorSettings>();
            var affiliateSettings = D.Resolve<AffiliateSettings>();

            var mediaAccountant = D.Resolve<IMediaAccountant>();
            var categoryService = D.Resolve<ICategoryService>();
            var antiforgery = D.Resolve<IAntiforgery>();
            var currencyService = D.Resolve<ICurrencyService>();
            var applicationConfig = D.Resolve<IApplicationConfig>();

            var categories = categoryService.Get(x => x.ParentId == 0).ToList();
            var logoUrl = generalSettings.LogoId > 0
                ? mediaAccountant.GetPictureUrl(generalSettings.LogoId)
                : D.Resolve<IGenesisEngine>().MapUrl(applicationConfig.DefaultLogoUrl, true);
            var faviconUrl = generalSettings.LogoId > 0
                ? mediaAccountant.GetPictureUrl(generalSettings.FaviconId)
                : D.Resolve<IGenesisEngine>().MapUrl(applicationConfig.DefaultFaviconUrl, true);
            var categoryDefaultName =
                LocalizationHelper.Localize("All Categories", D.Resolve<IGenesisEngine>().CurrentLanguage.CultureCode);
            var store = new StoreImplementation()
            {
                Url = WebHelper.GetUrlFromPath("/", generalSettings.StoreDomain, urlSettings.GetUrlProtocol()),
                Name = generalSettings.StoreName,
                Theme = new ThemeImplementation()
                {
                    Name = generalSettings.ActiveTheme,
                    Url = WebHelper.GetUrlFromPath("/" + generalSettings.ActiveTheme, generalSettings.StoreDomain, urlSettings.GetUrlProtocol()),
                },
                LogoUrl = logoUrl,
                FaviconUrl = faviconUrl,
                CurrentPage = D.Resolve<IGenesisEngine>().GetActiveRouteName(),
                CurrentUrl = D.Resolve<IGenesisEngine>().CurrentHttpContext.Request.GetDisplayUrl(),
                Categories = SelectListHelper.GetSelectItemList(categories, x => x.Id, x => x.Name, categoryDefaultName),
                WishlistEnabled = orderSettings.EnableWishlist,
                RepeatOrdersEnabled = orderSettings.AllowReorder,
                ReviewsEnabled = catalogSettings.EnableReviews,
                ReviewModificationAllowed = catalogSettings.AllowReviewModification,
                ActiveCurrencyCode = D.Resolve<IGenesisEngine>().CurrentCurrency.IsoCode,
                PrimaryCurrencyCode = currencyService.Get(localizationSettings.BaseCurrencyId)?.IsoCode,
                ActiveCurrencySymbol = D.Resolve<IGenesisEngine>().CurrentCurrency.GetSymbol(),
                XsrfToken = antiforgery.GetToken(),
                SoftwareVersion = AppVersionEvaluator.Version,
                SoftwareTitle = applicationConfig.AppName,
                HoneypotFieldName = securitySettings.HoneypotFieldName,
                VendorSignupEnabled = vendorSettings.EnableVendorSignup,
                VendorsEnabled = vendorSettings.EnableVendorSignup,
                ActiveLanguageCode = D.Resolve<IGenesisEngine>().CurrentLanguage.CultureCode
            };

            var currentUser = D.Resolve<IGenesisEngine>().CurrentUser;
            if (affiliateSettings.EnableAffiliates && currentUser.IsActiveAffiliate())
            {
                if (!generalSettings.HeadlessMode)
                {
                    store.AffiliateUrl = store.CurrentPage == RouteNames.SingleProduct ? currentUser.GetAffiliateUrl(store.CurrentUrl) : currentUser.GetAffiliateUrl();
                }
                store.AffiliateCode = currentUser.Guid.ToString();
            }
            if (!D.Resolve<IGenesisEngine>().CurrentHttpContext.IsTokenAuthenticated() && gdprSettings.ShowCookiePopup && !currentUser.IsAdministrator())
            {
                store.CookiePopupText = gdprSettings.CookiePopupText;
                store.UseConsentGroup = gdprSettings.UseConsentGroup;
                //if there was no consent provided, we show the popup
                store.ShowCookieConsent = CookieHelper.GetRequestCookie(applicationConfig.ConsentCookieName).IsNullEmptyOrWhiteSpace();
                if (gdprSettings.ConsentGroupId > 0 && gdprSettings.UseConsentGroup)
                {
                    var consentGroupService = D.Resolve<IConsentGroupService>();
                    var gdprService = D.Resolve<IGdprService>();
                    var consentGroup = consentGroupService.GetWithConsents(gdprSettings.ConsentGroupId);
                    if (consentGroup != null)
                    {
                        //has user already consented?
                        var consented = currentUser != null && gdprService.AreConsentsActedUpon(currentUser.Id,
                            consentGroup.Consents.Select(x => x.Id).ToArray());
                        store.ShowCookieConsent = store.ShowCookieConsent && !consented;
                        if (store.ShowCookieConsent)
                        {
                            store.ConsentGroup = new ConsentGroupImplementation()
                            {
                                Name = consentGroup.Name,
                                Description = consentGroup.Description,
                                Consents = new List<ConsentImplementation>()
                            };
                            foreach (var consent in consentGroup.Consents)
                            {
                                store.ConsentGroup.Consents.Add(new ConsentImplementation()
                                {
                                    Description = consent.Description,
                                    Title = consent.Title,
                                    IsRequired = consent.IsRequired,
                                    Id = consent.Id
                                });
                            }
                        }
                    }
                    else
                    {
                        //no need to use consent group..it's null anyways
                        store.UseConsentGroup = false;
                    }
                }
            }

            return store;
        }

        public override bool RenderInAdmin => true;

        public override bool RenderInPublic => true;
    }
}