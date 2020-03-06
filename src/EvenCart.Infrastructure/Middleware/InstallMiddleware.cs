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

using System.Threading.Tasks;
using EvenCart.Data.Database;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class InstallMiddleware
    {
        private readonly RequestDelegate _next;

        public InstallMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!DatabaseManager.IsDatabaseInstalled())
            {
                if (!IsInstallUrl(context.Request.Path.Value) && !RequestHelper.IsRequestForStaticResource())
                {
                    context.Response.Redirect("/install");
                    return;
                }
            }
            await _next.Invoke(context);
        }

        private bool IsInstallUrl(string url)
        {
            return url == "/install" || url == "/test-connection";
        }

    }
}