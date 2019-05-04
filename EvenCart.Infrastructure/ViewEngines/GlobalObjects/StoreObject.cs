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
using EvenCart.Infrastructure.ViewEngines.GlobalObjects.Implementations;
using EvenCart.Services.Extensions;
using EvenCart.Services.Gdpr;
using FluentValidation.Results;
using Microsoft.AspNetCore.Antiforgery;

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

            var mediaAccountant = DependencyResolver.Resolve<IMediaAccountant>();
            var categoryService = DependencyResolver.Resolve<ICategoryService>();
            var antiforgery = DependencyResolver.Resolve<IAntiforgery>();

            var categories = categoryService.Get(x => x.ParentCategoryId == 0).ToList();
            var logoUrl = "";
            if (generalSettings.LogoId > 0)
            {
                logoUrl = mediaAccountant.GetPictureUrl(generalSettings.LogoId);
            }

            var categoryDefaultName =
                LocalizationHelper.Localize("All Categories", ApplicationEngine.CurrentLanguageCultureCode);
            var store = new StoreImplementation()
            {
                Url = WebHelper.GetUrlFromPath("/", generalSettings.StoreDomain, urlSettings.GetUrlProtocol()),
                Name = generalSettings.StoreName,
                Theme = new ThemeImplementation()
                {
                    Name = "Default",
                    Url = WebHelper.GetUrlFromPath("/default", generalSettings.StoreDomain, urlSettings.GetUrlProtocol()),
                },
                LogoUrl = logoUrl,
                CurrentPage = ApplicationEngine.GetActiveRouteName(),
                Categories = SelectListHelper.GetSelectItemList(categories, x => x.Id, x => x.Name, categoryDefaultName),
                WishlistEnabled = orderSettings.EnableWishlist,
                RepeatOrdersEnabled = orderSettings.AllowReorder,
                ReviewsEnabled = catalogSettings.EnableReviews,
                ReviewModificationAllowed = catalogSettings.AllowReviewModification,
                ActiveCurrencyCode = ApplicationEngine.CurrentCurrency.IsoCode,
                XsrfToken = antiforgery.GetToken()
            };
            var currentUser = ApplicationEngine.CurrentUser;
            if (!RequestHelper.IsApiCall() && gdprSettings.ShowCookiePopup && !currentUser.IsAdministrator())
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