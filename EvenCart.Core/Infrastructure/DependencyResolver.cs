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
            return ServiceProvider.GetRequiredService<T>();
        }

        public static T Resolve<T>(object serviceKey)
        {
            return Container.Resolve<T>(serviceKey: serviceKey);
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