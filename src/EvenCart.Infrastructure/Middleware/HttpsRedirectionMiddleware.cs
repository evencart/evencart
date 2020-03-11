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

using System.Net;
using System.Threading.Tasks;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace EvenCart.Infrastructure.Middleware
{
    public class HttpsRedirectionMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpsRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (DatabaseManager.IsDatabaseInstalled())
            {
                var urlSettings = DependencyResolver.Resolve<UrlSettings>();
                if (urlSettings.EnableSsl)
                {
                    var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
                    if (context.Request.Scheme != "https")
                    {
                        if (RequestHelper.IsApiCall())
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                            await context.Response.WriteAsync("API calls must be made over Https");
                            return;
                        }
                        var getUrl = context.Request.GetEncodedPathAndQuery();
                        var newUrl = WebHelper.GetUrlFromPath(getUrl, generalSettings.StoreDomain, "https");
                        context.Response.Redirect(newUrl);
                        return;
                    }
                }
            }
            await _next.Invoke(context);
        }
        
    }
}