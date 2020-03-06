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
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Helpers
{
    public static class CookieHelper
    {
        public static void SetResponseCookie(string cookieName, string cookieValue, bool isPersistent)
        {
            ApplicationEngine.CurrentHttpContext.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
            {
                Expires = isPersistent ? (DateTimeOffset?) DateTimeOffset.UtcNow.AddDays(365): null
            });
        }

        public static void SetResponseCookie(string cookieName, string cookieValue, int expirationHours)
        {
            ApplicationEngine.CurrentHttpContext.Response.Cookies.Append(cookieName, cookieValue, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddHours(expirationHours)
            });
        }

        public static string GetRequestCookie(string cookieName)
        {
            return ApplicationEngine.CurrentHttpContext.Request.Cookies[cookieName];
        }
    }
}