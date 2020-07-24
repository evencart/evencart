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

using System.IO;
using System.Net;
using EvenCart.Core.Caching;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Tasks;
using EvenCart.Data.Database;
using EvenCart.Infrastructure.Caching;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Localization;
using EvenCart.Infrastructure.Middleware;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Services.Plugins;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
                DatabaseManager.ClearVersions();
                //upgrade to latest version
                DatabaseManager.UpgradeDatabase();

                //upgrade capabilities
                CapabilityHelper.UpgradeCapabilities();

                //mark plugin data
                var pluginAccountant = DependencyResolver.Resolve<IPluginAccountant>();
                var availablePlugins = pluginAccountant.GetAvailablePlugins(true);
                var installedVersions = DatabaseManager.GetInstalledVersions();
                foreach (var plugin in availablePlugins)
                {
                    var dbPlugin = plugin.LoadPluginInstance<DatabasePlugin>();
                    plugin.Dirty = installedVersions.ContainsKey(plugin.SystemName) &&
                                           dbPlugin.IsDatabaseUpgradeRequired(installedVersions[plugin.SystemName]);
                }
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.Headers;
                    var contentType = headers["Content-Type"];
                    if (contentType == "application/x-gzip")
                    {
                        if (context.File.Name.EndsWith("js.gz"))
                        {
                            contentType = "application/javascript";
                        }
                        else if (context.File.Name.EndsWith("css.gz"))
                        {
                            contentType = "text/css";
                        }
                        headers.Add("Content-Encoding", "gzip");
                        headers["Content-Type"] = contentType;
                    }
                }
            });

            //bundles directory
            var bundleDir = Path.Combine(hostingEnvironment.WebRootPath, "bundles");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(bundleDir),
                RequestPath = new PathString($"/bundles")
            });

            //get all the theme's directories, they'll be used for static files
            var themesDir = Path.Combine(hostingEnvironment.ContentRootPath, "Content", "Themes");
            var allThemes = Directory.GetDirectories(themesDir);
            foreach (var themeDir in allThemes)
            {

                var directoryInfo = new DirectoryInfo(themeDir);
                var assetDir =
                    Path.Combine(themesDir, themeDir, "Assets");
                if (!Directory.Exists(assetDir))
                    continue;
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(assetDir),
                    RequestPath = new PathString($"/{directoryInfo.Name}/assets")
                });
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(themesDir, themeDir, "Assets")),
                    RequestPath = new PathString($"/content/themes/{directoryInfo.Name}/assets")
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

        public static void UseRecaptcha(this IApplicationBuilder app)
        {
            app.UseMiddleware<GoogleRecaptchaMiddleware>();
        }

        public static void UseErrorLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
        }

        public static void UseAffiliateTracking(this IApplicationBuilder app)
        {
            app.UseMiddleware<AffiliateTrackingMiddleware>();
        }

        public static void UseStoreLoader(this IApplicationBuilder app)
        {
            app.UseMiddleware<StoreLoaderMiddleware>();
        }

        public static void UseHeadlessChecker(this IApplicationBuilder app)
        {
            app.UseMiddleware<HeadlessModeCheckerMiddleware>();
        }

        public static void InitializeCacheProviders(this IApplicationBuilder app)
        {
            DependencyResolver.Resolve<ICacheAccountant>().InitProviders();
        }
    }
}