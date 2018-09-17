using System;
using System.Linq;
using DryIoc;
using RoastedMarketplace.Core.Caching;
using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Core.Infrastructure.Utils;
using RoastedMarketplace.Core.Modules;
using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Infrastructure.Authentication;
using RoastedMarketplace.Infrastructure.Database;
using RoastedMarketplace.Infrastructure.Localization;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Providers;
using RoastedMarketplace.Infrastructure.ViewEngines;
using RoastedMarketplace.Services.Authentication;
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
            registrar.Register<IViewAccountant, ViewAccountant>(reuse: Reuse.Singleton);
            registrar.Register<IAppViewEngine, DefaultAppViewEngine>(reuse: Reuse.Singleton);

            //model mapper
            registrar.Register<IModelMapper, ModelMapper>(reuse: Reuse.Singleton);

            var asm = AssemblyLoader.GetAppDomainAssemblies();

            //services
            //to register services, we need to get all types from services assembly and register each of them;
            var serviceAssembly = asm.First(x => x.FullName.Contains("RoastedMarketplace.Services,"));
            var serviceTypes = serviceAssembly.GetTypes().
                Where(type => type.IsPublic && // get public types 
                              !type.IsAbstract && // which are not interfaces nor abstract
                              type.GetInterfaces().Length != 0);// which implementing some interface(s)

            registrar.RegisterMany(serviceTypes, Reuse.ScopedOrSingleton);

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

            var allModules = TypeFinder.ClassesOfType<IModule>();
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