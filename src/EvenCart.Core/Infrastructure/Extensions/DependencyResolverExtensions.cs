using System;
using System.Collections.Generic;
using DryIoc;

namespace EvenCart.Core.Infrastructure.Extensions
{
    public static class DependencyResolverExtensions
    {
        public static void RegisterMany<T>(this IRegistrator registrator, IEnumerable<Type> implTypes, Func<Type, string> serviceKeyFunc)
        {
            foreach (var implType in implTypes)
            {
                registrator.Register(typeof(T), implType, serviceKey: serviceKeyFunc?.Invoke(implType));
            }
        }
    }
}