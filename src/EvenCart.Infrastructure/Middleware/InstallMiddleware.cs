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