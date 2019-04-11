using System;
using System.Linq;
using System.Reflection;
using DryIoc;
using EvenCart.Core.Caching;
using EvenCart.Core.Config;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Plugins;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Database;
using EvenCart.Services.Authentication;
using EvenCart.Services.Emails;
using EvenCart.Services.Search;
using EvenCart.Services.Settings;
using EvenCart.Services.Users;
using EvenCart.Infrastructure.Authentication;
using EvenCart.Infrastructure.Database;
using EvenCart.Infrastructure.Emails;
using EvenCart.Infrastructure.Localization;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Components;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Plugins;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Routing.Parsers;
using EvenCart.Infrastructure.Theming;
using EvenCart.Infrastructure.ViewEngines;

namespace EvenCart.Infrastructure.DependencyContainer
{
    public class DependencyContainer : IDependencyContainer
    {
        public void RegisterDependencies(IRegistrator registrar)
        {
            // settings register for access across app
            registrar.Register<IDatabaseSettings, DatabaseSettings>(reuse: Reuse.Singleton);
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
            //email sender
            registrar.Register<IEmailSender, EmailSender>(reuse: Reuse.Transient);

            var asm = AssemblyLoader.GetAppDomainAssemblies();
            var allTypes = asm.Where(x => !x.IsDynamic).SelectMany(x =>
            {
                try
                {
                    return x.GetTypes();
                }
                catch (ReflectionTypeLoadException)
                {
                    return new Type[0];
                }
            }).ToList();
            //find all event consumer types
            var allConsumerTypes = allTypes
                .Where(type => type.IsPublic && // get public types 
                               type.GetInterfaces()
                                   .Any(x => x.IsAssignableTo(typeof(IFoundationEvent)) &&
                                             !type.IsAbstract));// which implementing some interface(s)
            //all consumers which are not interfaces
            registrar.RegisterMany(allConsumerTypes);

            //find all the model factories
            var allModelFactories = allTypes
                .Where(type => type.IsPublic && // get public types 
                               type.GetInterfaces()
                                   .Any(x => x.IsAssignableTo(typeof(IModelFactory)) &&
                                             !type.IsAbstract));// which implementing some interface(s)
            //all consumers which are not interfaces
            registrar.RegisterMany(allModelFactories);
            
            //capability providers
            var allCapabilityProviderTypes = allTypes
                .Where(type => type.IsPublic && // get public types 
                               type.GetInterfaces()
                                   .Any(x => x.IsAssignableTo(typeof(ICapabilityProvider)) &&
                                             !type.IsAbstract));// which implementing some interface(s)
            //all providers which are not interfaces
            registrar.RegisterMany(allCapabilityProviderTypes);

            //services
            //to register services, we need to get all types from services assembly and register each of them;
            var serviceAssembly = asm.First(x => x.FullName.Contains("EvenCart.Services,"));
            var serviceTypes = serviceAssembly.GetTypes().
                Where(type => type.IsPublic && // get public types 
                              !type.IsAbstract && // which are not interfaces nor abstract
                              type.GetInterfaces().Length != 0);// which implementing some interface(s)

            registrar.RegisterMany(serviceTypes, Reuse.Transient);

            //components
            //find all event consumer types
            var allComponents = allTypes
                .Where(type => type.IsPublic && // get public types 
                               !type.IsAbstract &&
                               type.IsAssignableTo(typeof(FoundationComponent)));// which implementing some interface(s)

            registrar.RegisterMany(allComponents, Reuse.Transient);

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

            registrar.Register<IAppAuthenticationService, AuthenticationService>(reuse: Reuse.Transient);

            var allModules = TypeFinder.ClassesOfType<IPlugin>();
            foreach (var moduleType in allModules)
            {
                var type = moduleType;
                registrar.Register(type, reuse: Reuse.Singleton);
            }
        }

        public int Priority { get; }
    }
}