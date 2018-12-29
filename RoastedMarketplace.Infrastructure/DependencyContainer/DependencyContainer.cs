using System;
using System.Linq;
using DryIoc;
using RoastedMarketplace.Core.Caching;
using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Providers;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Core.Plugins;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Infrastructure.Authentication;
using RoastedMarketplace.Infrastructure.Database;
using RoastedMarketplace.Infrastructure.Localization;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Components;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Plugins;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Routing.Parsers;
using RoastedMarketplace.Infrastructure.Theming;
using RoastedMarketplace.Infrastructure.ViewEngines;
using RoastedMarketplace.Services.Authentication;
using RoastedMarketplace.Services.Search;
using RoastedMarketplace.Services.Settings;

namespace RoastedMarketplace.Infrastructure.DependencyContainer
{
    public class DependencyContainer : IDependencyContainer
    {
        public void RegisterDependencies(IRegistrator registrar)
        {
            // settings register for access across app
            registrar.Register<IDatabaseSettings>(made: Made.Of(() => new DatabaseSettings()), reuse: Reuse.Singleton);
            //caching
            registrar.Register<ICacheProvider, DefaultCacheProvider>(reuse: Reuse.Singleton);
            //events
            registrar.Register<IEventPublisherService, EventPublisherService>(reuse: Reuse.Singleton);
            //file access
            registrar.Register<ILocalFileProvider, LocalFileProvider>(reuse: Reuse.Singleton);
            //localizer
            registrar.Register<ILocalizer, Localizer>(reuse: Reuse.ScopedOrSingleton);
            //view engine & friends
            registrar.Register<IViewAccountant, ViewAccountant>(reuse: Reuse.ScopedOrSingleton);
            registrar.Register<IAppViewEngine, DefaultAppViewEngine>(reuse: Reuse.Singleton);
            //media
            registrar.Register<IImageProcessor, ImageProcessor>(reuse: Reuse.Singleton);
            registrar.Register<IMediaAccountant, MediaAccountant>(reuse: Reuse.Singleton);
            //plugin loader
            registrar.Register<IPluginLoader, PluginLoader>(reuse: Reuse.Singleton);
            registrar.Register<IPluginAccountant, PluginAccountant>(reuse: Reuse.ScopedOrSingleton);
            //model mapper
            registrar.Register<IModelMapper, ModelMapper>(reuse: Reuse.Singleton);
            //routetemplate parser
            registrar.Register<IRouteTemplateParser, RouteTemplateParser>(reuse: Reuse.Singleton);
            registrar.Register<IDynamicRouteProvider, DynamicRouteProvider>(reuse: Reuse.ScopedOrSingleton);
            //themes
            registrar.Register<IThemeProvider, ThemeProvider>(reuse: Reuse.ScopedOrSingleton);
            //view compoenent
            registrar.Register<IViewComponentManager, ViewComponentManager>(reuse: Reuse.ScopedOrSingleton);
            //search query
            registrar.Register<ISearchQueryParserService, SearchQueryParserService>(reuse: Reuse.Scoped);
            //html processor
            registrar.Register<IHtmlProcessor, HtmlProcessor>(reuse: Reuse.Singleton);

            var asm = AssemblyLoader.GetAppDomainAssemblies();
            //find all event consumer types
            var allConsumerTypes = asm.SelectMany(x => x.GetTypes())
                .Where(type => type.IsPublic && // get public types 
                               type.GetInterfaces()
                                   .Any(x => x.IsGenericType &&
                                             x.IsAssignableTo(typeof(IFoundationEntityEvent)) && 
                                             !type.IsAbstract));// which implementing some interface(s)
            //all consumers which are not interfaces
            registrar.RegisterMany(allConsumerTypes);

            //services
            //to register services, we need to get all types from services assembly and register each of them;
            var serviceAssembly = asm.First(x => x.FullName.Contains("RoastedMarketplace.Services,"));
            var serviceTypes = serviceAssembly.GetTypes().
                Where(type => type.IsPublic && // get public types 
                              !type.IsAbstract && // which are not interfaces nor abstract
                              type.GetInterfaces().Length != 0);// which implementing some interface(s)

            registrar.RegisterMany(serviceTypes, Reuse.ScopedOrSingleton);

            //components
            //find all event consumer types
            var allComponents = asm.SelectMany(x => x.GetTypes())
                .Where(type => type.IsPublic && // get public types 
                               !type.IsAbstract &&
                               type.IsAssignableTo(typeof(FoundationComponent)));// which implementing some interface(s)

            registrar.RegisterMany(allComponents, Reuse.ScopedOrSingleton);

            //settings
            var allSettingTypes = TypeFinder.ClassesOfType<ISettingGroup>();
            foreach (var settingType in allSettingTypes)
            {
                var type = settingType;
                registrar.RegisterDelegate(type, resolver =>
                {
                    var instance = (ISettingGroup)Activator.CreateInstance(type);
                    resolver.Resolve<ISettingService>().LoadSettings(instance);
                    return instance;
                }, reuse: Reuse.Singleton);

            }

            registrar.Register<IAppAuthenticationService, AuthenticationService>(reuse: Reuse.ScopedOrSingleton);

            var allModules = TypeFinder.ClassesOfType<IPlugin>();
            foreach (var moduleType in allModules)
            {
                var type = moduleType;
                registrar.RegisterDelegate(type, resolver =>
                {
                    var instance = Activator.CreateInstance(type);
                    return instance;
                }, reuse: Reuse.Singleton);
            }
        }

        public int Priority { get; }
    }
}