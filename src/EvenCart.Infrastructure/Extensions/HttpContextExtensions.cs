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
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Pages;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.Breadcrumbs;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        private const string CurrentUserKey = "CurrentUser";
        private const string BreadcrumbKey = "BreadcrumbKey";
        private const string CurrentLanguageKey = "CurrentLanguageKey";
        private const string CurrentCurrencyKey = "CurrentCurrencyKey";
        private const string RequestSeoMetaKey = "RequestSeoMetaKey";
        private const string ApiTokenKey = "ApiTokenKey";
        private const string IsTokenAuthenticatedKey = "IsTokenAuthenticated";
        private const string CurrentAffiliateKey = "CurrentAffiliateKey";
        private const string CurrentStoreKey = "CurrentStoreKey";

        public static void SetCurrentCurrency(this HttpContext httpContext, Currency currency)
        {
            httpContext.Items[CurrentCurrencyKey] = currency;
        }

        public static Currency GetCurrentCurrency(this HttpContext httpContext)
        {
            return (Currency)httpContext.Items[CurrentCurrencyKey];
        }

        public static void SetCurrentLanguage(this HttpContext httpContext, Language language)
        {
            httpContext.Items[CurrentLanguageKey] = language;
        }

        public static Language GetCurrentLanguage(this HttpContext httpContext)
        {
            return (Language)httpContext.Items[CurrentLanguageKey];
        }

        public static void SetCurrentUser(this HttpContext httpContext, User user, bool isTokenValidation = false)
        {
            httpContext.Items[CurrentUserKey] = user;
            httpContext.Items[IsTokenAuthenticatedKey] = isTokenValidation;
        }

        public static User GetCurrentUser(this HttpContext httpContext)
        {
            return (User)httpContext.Items[CurrentUserKey];
        }

        public static void AppendToBreadcrumb(this HttpContext httpContext, BreadcrumbNode node)
        {
            var nodes = (List<BreadcrumbNode>)httpContext.Items[BreadcrumbKey];
            if (nodes == null)
            {
                nodes = new List<BreadcrumbNode>()
                {
                    new BreadcrumbNode()
                    {
                        DisplayText = LocalizationHelper.Localize("Home", ApplicationEngine.CurrentLanguage.CultureCode),
                        Url = ApplicationEngine.RouteUrl(RouteNames.Home, absoluteUrl: true)
                    }
                };
                httpContext.Items[BreadcrumbKey] = nodes;
            }
            nodes.Add(node);
        }

        public static List<BreadcrumbNode> GetBreadcrumb(this HttpContext httpContext)
        {
            var nodes = (List<BreadcrumbNode>)httpContext.Items[BreadcrumbKey];
            return nodes;
        }

        public static void SetRequestSeoMeta(this HttpContext httpContext, SeoMeta seoMeta)
        {
            httpContext.Items[RequestSeoMetaKey] = seoMeta;
        }

        public static SeoMeta GetRequestSeoMeta(this HttpContext httpContext)
        {
            return (SeoMeta)httpContext.Items[RequestSeoMetaKey];
        }

        public static void SetApiToken(this HttpContext httpContext, string apiToken)
        {
            httpContext.Items[ApiTokenKey] = apiToken;
        }

        public static string GetApiToken(this HttpContext httpContext)
        {
            return httpContext.Items[ApiTokenKey]?.ToString();
        }

        public static void SetCurrentAffiliate(this HttpContext httpContext, User affiliate)
        {
            httpContext.Items[CurrentAffiliateKey] = affiliate;
        }

        public static User GetCurrentAffiliate(this HttpContext httpContext)
        {
            return (User) httpContext.Items[CurrentAffiliateKey];
        }

        public static bool IsTokenAuthenticated(this HttpContext httpContext)
        {
            return httpContext.Items.ContainsKey(IsTokenAuthenticatedKey) && (bool) httpContext.Items[IsTokenAuthenticatedKey];
        }

        public static string GetReferer(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["Referer"];
        }

        public static Store GetCurrentStore(this HttpContext httpContext)
        {
            return (Store) httpContext.Items[CurrentStoreKey];
        }

        public static void SetCurrentStore(this HttpContext httpContext, Store store)
        {
            httpContext.Items[CurrentStoreKey] = store;
        }
    }
}