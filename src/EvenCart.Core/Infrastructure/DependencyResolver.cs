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
using System.Collections.Generic;
using DotEntity.Reflection;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;

namespace EvenCart.Core.Infrastructure
{
    public static class DependencyResolver
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return ServiceProvider == null ? default(T) : ServiceProvider.GetRequiredService<T>();
        }

        public static T Resolve<T>(object serviceKey, params object[] args)
        {
            return Container.Resolve<T>(serviceKey: serviceKey, ifUnresolved: IfUnresolved.ReturnDefault, null, args);
        }

        public static T ResolveOptional<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public static IEnumerable<T> ResolveMany<T>()
        {
            return ServiceProvider.GetServices<T>();
        }

        public static object Resolve(Type type)
        {
            return ServiceProvider.GetRequiredService(type);
        }

        public static object ResolveOptional(Type type, bool createIfNull = false)
        {
            var instance = ServiceProvider.GetService(type);
            if (instance != null || !createIfNull)
                return instance;
            instance = Instantiator.GetInstance(type);
            return instance;
        }
     
    }
}