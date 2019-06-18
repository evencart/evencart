using System.IO;
using System.Net;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Tasks;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Localization;
using EvenCart.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

namespace EvenCart.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseStatusPages(this IApplicationBuilder app)
        {
            if (!DatabaseManager.IsDatabaseInstalled())
                return;
            app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");

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
                        await response.WriteAsync("{ \"message\" : \"Unauthorized access\", \"success\" : \"false\" }");
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

                //upgrade capabilities
                CapabilityHelper.UpgradeCapabilities();
            }

        }

        public static void LoadLocalizations(this IApplicationBuilder app)
        {
            var localizer = DependencyResolver.Resolve<ILocalizer>();
            localizer.LoadLanguage("en-US");
        }

        public static void RunScheduledTasks(this IApplicationBuilder app)
        {
            var taskManager = DependencyResolver.Resolve<ITaskManager>();
            taskManager.Start();
        }

        public static void CheckInstallation(this IApplicationBuilder app)
        {
            app.UseMiddleware<InstallMiddleware>();
        }

        public static void UseHttps(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpsRedirectionMiddleware>();
        }

        public static void UseStaticFiles(this IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            app.UseStaticFiles();

            //bundles directory
            var bundleDir = Path.Combine(hostingEnvironment.WebRootPath, "Bundles");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(bundleDir),
                RequestPath = new PathString($"/bundles")
            });

#if DEBUG
            //bundles directory
            var samplesDir = Path.Combine(hostingEnvironment.WebRootPath, "samples");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(samplesDir),
                RequestPath = new PathString($"/samples")
            });
#endif

            //get all the theme's directories, they'll be used for static files
            var themesDir = Path.Combine(hostingEnvironment.ContentRootPath, "Content", "Themes");
            var allThemes = Directory.GetDirectories(themesDir);
            foreach (var themeDir in allThemes)
            {
                var directoryInfo = new DirectoryInfo(themeDir);
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(themesDir, themeDir, "Assets")),
                    RequestPath = new PathString($"/{directoryInfo.Name}/assets")
                });
            }

            //also plugin's assets directories
            var pluginsDir = Path.Combine(hostingEnvironment.ContentRootPath, "Plugins");
            var allPlugins = Directory.GetDirectories(pluginsDir);
            foreach (var pluginDir in allPlugins)
            {
                var directoryInfo = new DirectoryInfo(pluginDir);
                var assetDir = Path.Combine(pluginsDir, pluginDir, "Assets");
                if (!Directory.Exists(assetDir))
                    continue;
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(assetDir),
                    RequestPath = new PathString($"/plugins/{directoryInfo.Name}/assets")
                });
            }
        }

        public static void UseAntiforgeryTokens(this IApplicationBuilder app)
        {
            app.UseMiddleware<AntiforgeryValidationMiddleware>();
        }

        public static void UseIpFilter(this IApplicationBuilder app)
        {
            app.UseMiddleware<IpAddressValidationMiddleware>();
        }
    }
}