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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using DinkToPdf;
using DinkToPdf.Contracts;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Plugins;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure.Authentication;
using EvenCart.Infrastructure.Database;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Routing.Conventions;
using EvenCart.Infrastructure.ViewEngines;
using EvenCart.Services.Security;
using HtmlToPdfConverter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EvenCart.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static bool _dbHasSettings = false;
        public static void InitDbConnection(this IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            var dbSettings = new DatabaseSettings(hostingEnvironment);
            services.AddSingleton<IDatabaseSettings>(provider => dbSettings);
            _dbHasSettings = dbSettings.HasSettings();
            if (_dbHasSettings)
            {
                //initialize database
                DatabaseManager.InitDatabase(dbSettings);

                //upgrade to latest version without plugins
                DatabaseManager.UpgradeDatabase(true);

            }
        }

        public static void AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = ApplicationConfig.DefaultAuthenticationScheme;
                    options.DefaultChallengeScheme = ApplicationConfig.DefaultAuthenticationScheme;
                    options.DefaultSignInScheme = ApplicationConfig.DefaultAuthenticationScheme;
                })
                .AddCookie(ApplicationConfig.VisitorAuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString(ApplicationConfig.DefaultLoginUrl);
                    options.Events = new CookieAuthEvents();
                })
                .AddCookie(ApplicationConfig.DefaultAuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString(ApplicationConfig.DefaultLoginUrl);
                    options.AccessDeniedPath = new PathString(ApplicationConfig.DefaultLoginUrl);
                    options.Events = new CookieAuthEvents();
                })
                .AddCookie(ApplicationConfig.ExternalAuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString(ApplicationConfig.DefaultLoginUrl);
                    options.AccessDeniedPath = new PathString(ApplicationConfig.DefaultLoginUrl);
                });

            services.AddAuthentication(ApplicationConfig.ApiAuthenticationScheme)
                .AddJwtBearer(ApplicationConfig.ApiAuthenticationScheme, x =>
                {
                    var configuration = DependencyResolver.Resolve<IApplicationConfiguration>();
                    var key = Encoding.UTF8.GetBytes(configuration.GetSetting(ApplicationConfig.AppSettingsApiSecret));
                    var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = generalSettings.StoreDomain
                    };
                    x.Events = new JwtAuthEvents();
                });
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    ApplicationConfig.VisitorAuthenticationScheme,
                    ApplicationConfig.DefaultAuthenticationScheme,
                    ApplicationConfig.ApiAuthenticationScheme);
                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public static IMvcBuilder AddAppMvc(this IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            var mvcBuilder = services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
#if !DEBUGWS
                    if (DatabaseManager.IsDatabaseInstalled())
                    {
#endif
                        options.Conventions.Add((IControllerModelConvention)new AppRoutingConvention());
                        options.Conventions.Add((IActionModelConvention)new AppRoutingConvention());
                        options.ModelBinderProviders.Insert(0, new WidgetSettingsModelBinderProvider());
#if !DEBUGWS
                    }
#endif

                })
                .AddNewtonsoftJson()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                //load plugins
                .AddAppPlugins(hostingEnvironment)
                .AddViewOptions(options =>
                {
                    options.ViewEngines.Clear();
                    options.ViewEngines.Add(new DefaultAppViewEngine());
                })
                //sets compatibility to 3.0
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                //add controllers as services
                .AddControllersAsServices();
            return mvcBuilder;
        }

        public static void AddAppRouting(this IServiceCollection services)
        {
            //use lowercase urls
            services.AddRouting(options => options.LowercaseUrls = true);
        }
        public static void AddGlobalSingletons(this IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            if (!ApplicationEngine.IsTestEnv(hostingEnvironment))
            {
                //add httpcontext accessor
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }
            //add action context
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //url helper
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        }

        public static IMvcBuilder AddAppPlugins(this IMvcBuilder mvcBuilder, IHostingEnvironment hostingEnvironment)
        {
            PluginLoader.Init(hostingEnvironment);
            PluginLoader.LoadAvailablePlugins();
            var pluginInfos = PluginLoader.GetAvailablePlugins();
            if (_dbHasSettings)
                pluginInfos.UpdateInstallStatus();
            mvcBuilder.ConfigureApplicationPartManager(manager =>
            {
                foreach (var pluginInfo in pluginInfos)
                {
                    manager.ApplicationParts.Add(new AssemblyPart(pluginInfo.Assembly));
                }
            });
            return mvcBuilder;
        }

        public static void AddAntiforgeryTokens(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "__RequestVerificationToken";
                options.Cookie.Name = "_xsrf";
            });
        }

        public static void EnableResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();
        }

        public static IServiceCollection AddHtmlToPdfConverter(this IServiceCollection services)
        {
#if DEBUGWS
            return services;
#endif

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IPdfConverter, PdfConverter>();

            var context = new CustomAssemblyLoadContext();
            var projectRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var path = Path.Combine(projectRootFolder, "NativeLibs", RuntimeInformation.ProcessArchitecture.ToString(), "libwkhtmltox.dll");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(projectRootFolder, "NativeLibs", RuntimeInformation.ProcessArchitecture.ToString(), "libwkhtmltox.so");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = Path.Combine(projectRootFolder, "NativeLibs", RuntimeInformation.ProcessArchitecture.ToString(), "libwkhtmltox.dylib");
            }

            context.LoadUnmanagedLibrary(path);

            return services;
        }
    }
}