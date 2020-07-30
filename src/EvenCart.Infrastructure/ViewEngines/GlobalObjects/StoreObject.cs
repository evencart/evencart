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
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Products;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using EvenCart.Services.Cultures;
using EvenCart.Services.Extensions;
using EvenCart.Services.Gdpr;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.Extensions;

namespace EvenCart.Infrastructure.ViewEngines.GlobalObjects
{
    public class StoreObject : GlobalObject
    {
        public override object GetObject()
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var catalogSettings = DependencyResolver.Resolve<CatalogSettings>();
            var orderSettings = DependencyResolver.Resolve<OrderSettings>();
            var urlSettings = DependencyResolver.Resolve<UrlSettings>();
            var gdprSettings = DependencyResolver.Resolve<GdprSettings>();
            var localizationSettings = DependencyResolver.Resolve<LocalizationSettings>();
            var securitySettings = DependencyResolver.Resolve<SecuritySettings>();
            var vendorSettings = DependencyResolver.Resolve<VendorSettings>();
            var affiliateSettings = DependencyResolver.Resolve<AffiliateSettings>();

            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var categoryService = DependencyResolver.Resolve<ICategoryService>();
            var antiforgery = DependencyResolver.Resolve<IAntiforgery>();
            var currencyService = DependencyResolver.Resolve<ICurrencyService>();

            var categories = categoryService.Get(x => x.ParentId == 0).ToList();
            var logoUrl = generalSettings.LogoId > 0
                ? mediaAccountant.GetPictureUrl(generalSettings.LogoId)
                : ApplicationEngine.MapUrl(ApplicationConfig.DefaultLogoUrl, true);
            var faviconUrl = generalSettings.LogoId > 0
                ? mediaAccountant.GetPictureUrl(generalSettings.FaviconId)
                : ApplicationEngine.MapUrl(ApplicationConfig.DefaultFaviconUrl, true);
            var categoryDefaultName =
                LocalizationHelper.Localize("All Categories", ApplicationEngine.CurrentLanguage.CultureCode);
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
                CurrentPage = ApplicationEngine.GetActiveRouteName(),
                CurrentUrl = ApplicationEngine.CurrentHttpContext.Request.GetDisplayUrl(),
                Categories = SelectListHelper.GetSelectItemList(categories, x => x.Id, x => x.Name, categoryDefaultName),
                WishlistEnabled = orderSettings.EnableWishlist,
                RepeatOrdersEnabled = orderSettings.AllowReorder,
                ReviewsEnabled = catalogSettings.EnableReviews,
                ReviewModificationAllowed = catalogSettings.AllowReviewModification,
                ActiveCurrencyCode = ApplicationEngine.CurrentCurrency.IsoCode,
                PrimaryCurrencyCode = currencyService.Get(localizationSettings.BaseCurrencyId)?.IsoCode,
                ActiveCurrencySymbol = ApplicationEngine.CurrentCurrency.GetSymbol(),
                XsrfToken = antiforgery.GetToken(),
                SoftwareVersion = AppVersionEvaluator.Version,
                SoftwareTitle = ApplicationConfig.AppName,
                HoneypotFieldName = securitySettings.HoneypotFieldName,
                VendorSignupEnabled = vendorSettings.EnableVendorSignup,
                VendorsEnabled = vendorSettings.EnableVendorSignup,
                ActiveLanguageCode = ApplicationEngine.CurrentLanguage.CultureCode
            };

            var currentUser = ApplicationEngine.CurrentUser;
            if (affiliateSettings.EnableAffiliates && currentUser.IsActiveAffiliate())
            {
                if (!generalSettings.HeadlessMode)
                {
                    store.AffiliateUrl = store.CurrentPage == RouteNames.SingleProduct ? currentUser.GetAffiliateUrl(store.CurrentUrl) : currentUser.GetAffiliateUrl();
                }
                store.AffiliateCode = currentUser.Guid.ToString();
            }
            if (!ApplicationEngine.CurrentHttpContext.IsTokenAuthenticated() && gdprSettings.ShowCookiePopup && !currentUser.IsAdministrator())
            {
                store.CookiePopupText = gdprSettings.CookiePopupText;
                store.UseConsentGroup = gdprSettings.UseConsentGroup;
                //if there was no consent provided, we show the popup
                store.ShowCookieConsent = CookieHelper.GetRequestCookie(ApplicationConfig.ConsentCookieName).IsNullEmptyOrWhiteSpace();
                if (gdprSettings.ConsentGroupId > 0 && gdprSettings.UseConsentGroup)
                {
                    var consentGroupService = DependencyResolver.Resolve<IConsentGroupService>();
                    var gdprService = DependencyResolver.Resolve<IGdprService>();
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