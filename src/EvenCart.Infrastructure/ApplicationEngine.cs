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

using System;
using System.IO;
using System.Linq;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Plugins;
using EvenCart.Core.Services;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Data.Extensions;
using EvenCart.Services.Authentication;
using EvenCart.Services.Cultures;
using EvenCart.Services.Extensions;
using EvenCart.Services.Purchases;
using EvenCart.Infrastructure.DependencyContainer;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Theming;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
#if !DEBUGWS
using EvenCart.Infrastructure.Routing;
#endif

namespace EvenCart.Infrastructure
{
    public static class ApplicationEngine
    {
        #region Initialization
        public static IServiceProvider ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            //register singletons
            services.AddGlobalSingletons(hostingEnvironment);

            if (!IsTestEnv(hostingEnvironment))
            {
                //init db connection
                services.InitDbConnection(hostingEnvironment);

                services.AddDataProtection()
                    .PersistKeysToFileSystem(
                        new DirectoryInfo(ApplicationConfig.SecureDataDirectory));

                //antiforgery
                services.AddAntiforgeryTokens();

                //add authentications
                services.AddAppAuthentication();

                //response compression
                services.EnableResponseCompression();

                //add MVC and routing convention for api access
                services.AddAppMvc(hostingEnvironment);
                services.AddAppRouting();

                //html to pdf
                services.AddHtmlToPdfConverter();

                //add any services from plugins
                var availablePlugins = PluginLoader.GetAvailablePlugins();
                foreach (var ap in availablePlugins.Where(x => x.Installed))
                    ap.Startup?.ConfigureServices(services, hostingEnvironment);
            }

            //fire up dependency injector
            var container = new Container();
            var serviceProvider = container
                .WithDependencyInjectionAdapter(services)
                .ConfigureServiceProvider<CompositionRoot>();

            //set dependency resolver for core functions
            DependencyResolver.ServiceProvider = serviceProvider;
            DependencyResolver.Container = serviceProvider.GetService<IContainer>();
            return serviceProvider;
        }

        private static IHostingEnvironment _hostingEnvironment;
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _hostingEnvironment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //error logger
            app.UseErrorLogger();
#if DEBUGWS
            if (DatabaseManager.IsDatabaseInstalled())
            {
                //store loader
                app.UseStoreLoader();
            }
#endif
#if !DEBUGWS
            app.CheckInstallation();

            //use static files
            app.UseStaticFiles(_hostingEnvironment);

            if (DatabaseManager.IsDatabaseInstalled())
            {
                //ip filtering
                app.UseIpFilter();

                //store loader
                app.UseStoreLoader();
                
                //https redirection
                app.UseHttps();

                //use response pages
                app.UseStatusPages();

                //init database
                app.InitializeDatabase();

                //affiliate tracking
                app.UseAffiliateTracking();

                //use authentication
                app.UseAppAuthentication();

                //anti-forgery validation
                app.UseAntiforgeryTokens();

                //recaptcha
                app.UseRecaptcha();

                //add any middlewares from plugins
                var availablePlugins = PluginLoader.GetAvailablePlugins();
                foreach (var ap in availablePlugins.Where(x => x.ActiveStoreIds != null && x.ActiveStoreIds.Any()))
                {
                    ap.Startup?.Configure(app);
                }

            }
            
#endif
                app.UseResponseCompression();


            //use mvc
            app.UseMvc(builder =>
            {
#if !DEBUGWS
                builder.Routes.Add(new AppRouter(builder.DefaultHandler));
#endif
            });

            //run the schedule tasks
            app.RunScheduledTasks();

            //load language files
            app.LoadLocalizations();
        }

        #endregion

        #region Helpers

        private static IUrlHelperFactory _urlHelperFactory;
        public static string RouteUrl(string routeName, object values = null, bool absoluteUrl = false)
        {
            _urlHelperFactory = _urlHelperFactory ?? DependencyResolver.Resolve<IUrlHelperFactory>();
            var urlHelper = _urlHelperFactory.GetUrlHelper(DependencyResolver.Resolve<IActionContextAccessor>().ActionContext);
            var relativeUrl = urlHelper.RouteUrl(routeName, values);
            if (!absoluteUrl)
                return relativeUrl;
            var generalSettings = DependencyResolver.Resolve<GeneralSettings>();
            var urlSettings = DependencyResolver.Resolve<UrlSettings>();
            return WebHelper.GetUrlFromPath(relativeUrl, generalSettings.StoreDomain, urlSettings.GetUrlProtocol());
        }

        

        public static string MapUrl(string path, bool isWebRootPath = true)
        {
            if (path.StartsWith("~/"))
            {
                //relative to absolute
                path = ServerHelper.MapPath(path, isWebRootPath);
            }
            // path = c:\\www\\Content\\...
            // content root = c:\\www
            var contentRootPath = isWebRootPath ? _hostingEnvironment.WebRootPath : _hostingEnvironment.ContentRootPath;
            var relativePath = path.Replace(contentRootPath, "");
            return relativePath.Replace("\\", "/");
        }
        /// <summary>
        /// Checks if the user is inside administration area
        /// </summary>
        public static bool IsAdmin()
        {
            var isAdmin = DependencyResolver.Resolve<IActionContextAccessor>().ActionContext.IsAdminArea();
            return isAdmin;
        }

        public static string GetActiveRouteName()
        {
            _urlHelperFactory = _urlHelperFactory ?? DependencyResolver.Resolve<IUrlHelperFactory>();
            var urlHelper = _urlHelperFactory.GetUrlHelper(DependencyResolver.Resolve<IActionContextAccessor>().ActionContext);
            urlHelper.ActionContext.ActionDescriptor.Properties.TryGetValue("RouteName", out object routeName);
            return routeName?.ToString() ?? "";
        }

        public static bool IsTestEnv(IHostingEnvironment hostingEnvironment = null)
        {
            _hostingEnvironment = _hostingEnvironment ?? hostingEnvironment;
            return _hostingEnvironment.IsEnvironment(ApplicationConfig.TestEnvironmentName);
        }
        #endregion

        public static HttpContext CurrentHttpContext => DependencyResolver.Resolve<IHttpContextAccessor>().HttpContext;

        public static User CurrentUser => DependencyResolver.Resolve<IAppAuthenticationService>().GetCurrentUser();

        public static ThemeInfo ActiveTheme => DependencyResolver.Resolve<IThemeProvider>().GetActiveTheme();

        public static User CurrentAffiliate => CurrentHttpContext.GetCurrentAffiliate();

        public static Store CurrentStore => CurrentHttpContext?.GetCurrentStore();

        public static string CurrentLanguageCultureCode => "en-US";

        public static Currency CurrentCurrency
        {
            get
            {
                var currency = CurrentHttpContext.GetCurrentCurrency();
                if (currency != null && currency.Published)
                    return currency;
                var currentCurrencyId = CurrentUser?.ActiveCurrencyId ?? DependencyResolver.Resolve<LocalizationSettings>().PrimaryCurrencyId;
                var currencyService = DependencyResolver.Resolve<ICurrencyService>();
                if (currentCurrencyId > 0)
                    currency = currencyService.Get(currentCurrencyId);

                if (currency == null || !currency.Published)
                    currency = currencyService.FirstOrDefault(x => x.Published);
                CurrentHttpContext.SetCurrentCurrency(currency);
                return currency;
            }
        }

        private static Currency _baseCurrency;
        public static Currency BaseCurrency
        {
            get
            {
                if (_baseCurrency != null)
                    return _baseCurrency;
                var baseCurrencyId = DependencyResolver.Resolve<LocalizationSettings>().BaseCurrencyId;
                var currencyService = DependencyResolver.Resolve<ICurrencyService>();
                if (baseCurrencyId > 0)
                    _baseCurrency = currencyService.Get(baseCurrencyId);
                _baseCurrency = _baseCurrency ?? currencyService.FirstOrDefault(x => true);
                return _baseCurrency;
            }
        }

        public static LoginStatus GuestSignIn()
        {
            //if we don't have any user, then only we'll do a guest signin
            if (CurrentUser != null)
                return LoginStatus.Success;
            var authenticationService = DependencyResolver.Resolve<IAppAuthenticationService>();
            return authenticationService.GuestSignIn();
        }

        public static LoginStatus SignIn(string email, string name, bool rememberMe, bool tokenAuth = false)
        {
            if (CurrentUser != null && !CurrentUser.IsVisitor())
                return LoginStatus.Success; //user is already logged in

            var isVisitor = CurrentUser != null && CurrentUser.IsVisitor();
            var visitorId = isVisitor ? CurrentUser.Id : 0;

            var authenticationService = DependencyResolver.Resolve<IAppAuthenticationService>();
            authenticationService.SignOut();
            var signinStatus = authenticationService.SignIn(
                tokenAuth ? ApplicationConfig.ApiAuthenticationScheme : ApplicationConfig.DefaultAuthenticationScheme,
                email, name, rememberMe);
            if (signinStatus != LoginStatus.Success)
                return signinStatus;

            //if we are here, the login was successful. If already logged in user was a visitor, we'll move the cart items 
            //from old user to new user
            if (isVisitor)
            {
                //move all the cart items of guest user to logged in user
                var cartService = DependencyResolver.Resolve<ICartService>();
                var cartItemService = DependencyResolver.Resolve<ICartItemService>();
                var visitorCart = cartService.GetCart(visitorId);
                if (visitorCart.CartItems.Any())
                {
                    Transaction.Initiate(transaction =>
                    {
                        //move the cart items to the current user's cart
                        var registeredUserCart = cartService.GetCart(CurrentUser.Id);
                        foreach (var cItem in visitorCart.CartItems)
                        {
                            cItem.CartId = registeredUserCart.Id;
                            CartItem sameCartItem = null;
                            if ((sameCartItem = registeredUserCart.CartItems.FirstOrDefault(x =>
                                    x.ProductId == cItem.ProductId && x.AttributeJson == cItem.AttributeJson)) != null)
                            {
                                sameCartItem.Quantity++;
                                cartItemService.Update(sameCartItem, transaction);
                                cartItemService.Delete(cItem, transaction);
                            }
                            else
                            {
                                cartItemService.Update(cItem, transaction);
                            }

                        }
                    });
                }
            }
            return signinStatus;
        }

        public static LoginStatus ImitationModeSignIn(string email)
        {
            var authenticationService = DependencyResolver.Resolve<IAppAuthenticationService>();
            return authenticationService.ImitationModeSignIn(email, CurrentUser.Email);
        }
    }
}