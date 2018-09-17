using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Infrastructure.Localization;

namespace RoastedMarketplace.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseStatusPages(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                var request = context.HttpContext.Request;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    if (!RequestHelper.IsApiCall())
                        response.Redirect(ApplicationConfig.DefaultLoginUrl + "?ReturnUrl=" + request.GetEncodedUrl());
                    else
                    {
                        response.ContentType = "application/json";
                        await response.WriteAsync("{ message : \"Unauthorized access\" }");
                    }
                }
            });
        }

        public static void UseAppAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware<AuthenticationMiddleware>();
        }

        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            var databaseSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            if (databaseSettings.HasSettings())
            {
                //initialize database
                DatabaseManager.InitDatabase(databaseSettings);

                //upgrade to latest version
                DatabaseManager.UpgradeDatabase();
            }

        }

        public static void LoadLocalizations(this IApplicationBuilder app)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            localizer.LoadLanguage("en-US");
        }
    }
}