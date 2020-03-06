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
using System.Web;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Routing;

namespace EvenCart.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static bool IsImitator(this User user, out string imitator)
        {
            imitator = user.GetMeta<string>(ApplicationConfig.ImitatorKey);
            return !imitator.IsNullEmptyOrWhiteSpace();
        }

        public static string GetAffiliateUrl(this User user, string baseUrl = null)
        {
            baseUrl = baseUrl ?? ApplicationEngine.RouteUrl(RouteNames.Home, absoluteUrl: true);
            var uri = new UriBuilder(baseUrl);
            var queryParameters = HttpUtility.ParseQueryString(uri.Query);
            queryParameters[ApplicationConfig.AffiliateIdQueryStringParameterName] = user.Guid.ToString();
            uri.Query = queryParameters.ToString();
            return uri.ToString();
        }
    }
}