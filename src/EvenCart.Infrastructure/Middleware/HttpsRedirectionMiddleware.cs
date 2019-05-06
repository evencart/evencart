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