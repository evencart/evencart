using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace RoastedMarketplace.Core.Infrastructure
{
    public static class DependencyResolver
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static T Resolve<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
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

        public static object ResolveOptional(Type type)
        {
            return ServiceProvider.GetService(type);
        }
    }
}