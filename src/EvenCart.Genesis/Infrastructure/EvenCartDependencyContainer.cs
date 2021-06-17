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
using System.Linq;
using System.Reflection;
using DryIoc;
using EvenCart.Genesis.ViewEngines;
using Genesis.Modules.Emails;
using Genesis.Infrastructure.DependencyManager;
using Genesis.Infrastructure.Types;
using Genesis.MediaServices;
using Genesis.Modules.DataTransfer;
using Genesis.Modules.Localization;
using Genesis.Modules.Search;
using Genesis.Modules.Users;
using Genesis.Routing;
using Genesis.ViewEngines;

namespace Genesis.Infrastructure
{
    public class EvenCartDependencyContainer : DependencyContainer
    {
        public override void RegisterDependencies(IRegistrator registrar)
        {
            base.RegisterDependencies(registrar);

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
            })
                .Where(x => x.IsPublic && !x.IsAbstract).ToList();

            
            registrar.Register<IMediaAccountant, MediaAccountant>(reuse: Reuse.Singleton);
            
            registrar.Register<IDynamicRouteProvider, DynamicRouteProvider>(reuse: Reuse.ScopedOrSingleton);
           
            registrar.Register<ISearchQueryParserService, SearchQueryParserService>(reuse: Reuse.Scoped);
          
            ////conection accountant
            registrar.Register<IConnectionAccountant, ConnectionAccountant>(Reuse.Transient);

            var serviceTypes = allTypes.Where(x => x.Assembly.FullName.StartsWith("Genesis") || x.Assembly.FullName.StartsWith("EvenCart.Genesis"))
                .Where(type => type.IsPublic &&
                               !type.IsAbstract &&
                               type.GetCustomAttribute<AutoResolvableAttribute>() != null).ToList();

            registrar.RegisterMany(serviceTypes, Reuse.Transient, ifAlreadyRegistered: IfAlreadyRegistered.Keep);

            //currency providers
            var allCurrencyProviders = allTypes
                .Where(type => type.IsPublic && // get public types 
                               type.GetInterfaces()
                                   .Any(x => x.IsAssignableTo(typeof(ICurrencyRateProvider))));// which implementing some interface(s)
            //all providers which are not interfaces
            registrar.RegisterMany<ICurrencyRateProvider>(allCurrencyProviders, type => type.FullName);
            
            registrar.Register<IViewAccountant, EvenCartViewAccountant>(reuse: Reuse.Singleton, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
            registrar.Register<IUserService, EvenCartUserService>(ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        }

        public void RegisterDependenciesIfActive(IRegistrator registrar)
        {

        }

        public int Priority { get; }
    }
}