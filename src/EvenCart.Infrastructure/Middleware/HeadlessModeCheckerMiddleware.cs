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
using EvenCart.Core.Data;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class HeadlessModeCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public HeadlessModeCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            if (generalSettings.HeadlessMode)
            {
                if (!RequestHelper.IsApiCall() && !IsHeadlessUrl(context.Request.Path.Value))
                {
                    var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(dataSerializer.Serialize(new
                    {
                        success = false,
                        message = "The resource is not found on server"
                    }));
                    return;
                }
            }
            await _next.Invoke(context);
        }

        private bool IsHeadlessUrl(string url)
        {
            return url == "/login" || url == "/logout" || url.StartsWith($"/{ApplicationConfig.AdminAreaName}");
        }
    }
}