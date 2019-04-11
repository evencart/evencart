using System;
using System.IO;
using System.Linq;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Cultures;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Services.Authentication;
using EvenCart.Services.Cultures;
using EvenCart.Services.Extensions;
using EvenCart.Services.Purchases;
using EvenCart.Infrastructure.DependencyContainer;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Theming;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace EvenCart.Infrastructure
{
    public static class ApplicationEngine
    {
        #region Initialization
        public static IServiceProvider ConfigureServices(IServiceCollection services, IHostingEnvironment hostingEnvironment)
        {
            //add authentications
            services.AddAppAuthentication();

            //register singletons
            services.AddGlobalSingletons();

            //add MVC and routing convention for api access
            services.AddAppMvc(hostingEnvironment);
            services.AddAppRouting();

            //fire up dependency injector
            var container = new Container();
            var serviceProvider = container
                .WithDependencyInjectionAdapter(services,
                    throwIfUnresolved: type => type.Name.EndsWith("Controller"))
                .ConfigureServiceProvider<CompositionRoot>();

            //set dependency resolver for core functions
            DependencyResolver.ServiceProvider = serviceProvider;
            DependencyResolver.Container = container;
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
            //use response pages
            app.UseStatusPages();

            app.UseStaticFiles();

            //get all the theme's directories, they'll be used for static files
            var themesDir = Path.Combine(_hostingEnvironment.ContentRootPath, "Content", "Themes");
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
            var pluginsDir = Path.Combine(_hostingEnvironment.ContentRootPath, "Plugins");
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
            //init database
            app.InitializeDatabase();
            
            //use authentication
            app.UseAppAuthentication();

            //use mvc
            app.UseMvc(builder =>
            {
                builder.Routes.Add(new AppRouter(builder.DefaultHandler));
            });

            //load language files
            app.LoadLocalizations();

            //run the schedule tasks
            app.RunScheduledTasks();
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
            return WebHelper.GetUrlFromPath(relativeUrl, generalSettings.StoreDomain);
        }

        public static string MapPath(string relativePath, bool isWebRootPath = false)
        {
            if (!relativePath.StartsWith("~/"))
                return relativePath;

            return relativePath.Replace("~",
                isWebRootPath ? _hostingEnvironment.WebRootPath : _hostingEnvironment.ContentRootPath).Replace("/", "\\");
        }

        public static string MapUrl(string path, bool isWebRootPath = true)
        {
            if (path.StartsWith("~/"))
            {
                //relative to absolute
                path = MapPath(path, isWebRootPath);
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

        #endregion

        public static HttpContext CurrentHttpContext => DependencyResolver.Resolve<IHttpContextAccessor>().HttpContext;

        public static User CurrentUser => DependencyResolver.Resolve<IAppAuthenticationService>().GetCurrentUser();

        public static ThemeInfo ActiveTheme => DependencyResolver.Resolve<IThemeProvider>().GetActiveTheme();

        public static string CurrentLanguageCultureCode => "fr-GB";

        public static Currency CurrentCurrency
        {
            get
            {
                var currency = CurrentHttpContext.GetCurrentCurrency();
                if (currency != null)
                    return currency;
                var currentCurrencyId = CurrentUser?.ActiveCurrencyId ?? DependencyResolver.Resolve<LocalizationSettings>().PrimaryCurrencyId;
                var currencyService = DependencyResolver.Resolve<ICurrencyService>();
                if (currentCurrencyId > 0)
                    currency = currencyService.Get(currentCurrencyId);
                currency = currency ?? currencyService.FirstOrDefault(x => true);
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

        public static LoginStatus SignIn(string email, string name, bool rememberMe)
        {
            if (CurrentUser != null && !CurrentUser.IsVisitor())
                return LoginStatus.Success; //user is already logged in

            var isVisitor = CurrentUser != null && CurrentUser.IsVisitor();
            var visitorId = isVisitor ? CurrentUser.Id : 0;

            var authenticationService = DependencyResolver.Resolve<IAppAuthenticationService>();
            authenticationService.SignOut();
            var signinStatus = authenticationService.SignIn(email, name, rememberMe);
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