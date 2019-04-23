using System;
using System.Net;
using System.Threading.Tasks;
using EvenCart.Data.Database;
using EvenCart.Infrastructure.Routing;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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