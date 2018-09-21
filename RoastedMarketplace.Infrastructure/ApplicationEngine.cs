using System;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Data.Entity.Users;
using RoastedMarketplace.Infrastructure.DependencyContainer;
using RoastedMarketplace.Infrastructure.Extensions;
using RoastedMarketplace.Infrastructure.Theming;
using RoastedMarketplace.Services.Authentication;

namespace RoastedMarketplace.Infrastructure
{
    public static class ApplicationEngine
    {
        #region Initialization
        public static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //initialize modules
            ModuleEngine.Initialize();
            
            //add authentications
            services.AddAppAuthentication();

            //register singletons
            services.AddGlobalSingletons();

            //add MVC and routing convention for api access
            services.AddAppMvc();
            services.AddAppRouting();

            //fire up dependency injector
            var serviceProvider = new Container()
                .WithDependencyInjectionAdapter(services,
                    throwIfUnresolved: type => type.Name.EndsWith("Controller"))
                .ConfigureServiceProvider<CompositionRoot>();

            //set dependency resolver for core functions
            DependencyResolver.ServiceProvider = serviceProvider;

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

            app.UseStaticFiles();

            //init database
            app.InitializeDatabase();

            //use response pages
            app.UseStatusPages();

            //use authentication
            app.UseAppAuthentication();

            //use mvc
            app.UseMvc();

            //load language files
            app.LoadLocalizations();
        }

        #endregion

        #region Helpers

        private static IUrlHelperFactory _urlHelperFactory;
        public static string RouteUrl(string routeName, object values = null)
        {
            _urlHelperFactory = _urlHelperFactory ?? DependencyResolver.Resolve<IUrlHelperFactory>();
            var urlHelper = _urlHelperFactory.GetUrlHelper(DependencyResolver.Resolve<IActionContextAccessor>().ActionContext);
            return urlHelper.RouteUrl(routeName, values);
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
            // path = c:\\www\\Content\\...
            // content root = c:\\www
            var contentRootPath = isWebRootPath ? _hostingEnvironment.WebRootPath : _hostingEnvironment.ContentRootPath;
            var relativePath = path.Replace(contentRootPath, "");
            return relativePath.Replace("\\", "/");
        }

        public static bool IsAdmin()
        {
            var isAdmin = DependencyResolver.Resolve<IActionContextAccessor>().ActionContext.IsAdminArea();
            return isAdmin;
        }

        #endregion

        public static HttpContext CurrentHttpContext => DependencyResolver.Resolve<IHttpContextAccessor>().HttpContext;

        public static User CurrentUser => DependencyResolver.Resolve<IAppAuthenticationService>().GetCurrentUser();

        public static ThemeInfo ActiveTheme => new ThemeInfo();//todo:set this

        
    }
}