using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoastedMarketplace.Core.Infrastructure.Utils
{
    public sealed class TypeFinder
    {
        private static TypeFinder _finder
        {
            get { return Singleton<TypeFinder>.Instance; }
        }


        private static IList<Assembly> _allAssemblies;
        
        IList<Type> OfType<T>(bool excludeAbstract = true)
        {
            var loadedTypes = new List<Type>();
            foreach (var assembly in _allAssemblies)
            {
                //exclude if it's a system assembly
                if(AssemblyLoader.IsSystemAssembly(assembly.FullName))
                    continue;
                try
                {
                    //if error occurs while loading the assembly, continue or throw error
                    var types = assembly.GetTypes().Where(x => x.IsClass).ToList();
                    foreach (var type in types)
                    {
                        if(excludeAbstract && type.IsClass && type.IsAbstract)
                            continue;

                        if (typeof(T).IsAssignableFrom(type) || typeof(T).IsGenericType)
                        {
                            loadedTypes.Add(type);
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return loadedTypes;
        }

        public static Type OfType<T>(Assembly assembly, bool excludeAbstract = true)
        {
            //exclude if it's a system assembly
            if (AssemblyLoader.IsSystemAssembly(assembly.FullName))
                return null;
            try
            {
                //if error occurs while loading the assembly, continue or throw error
                var types = assembly.GetTypes().Where(x => x.IsClass).ToList();
                foreach (var type in types)
                {
                    if (excludeAbstract && type.IsClass && type.IsAbstract)
                        continue;

                    if (typeof(T).IsAssignableFrom(type) || typeof(T).IsGenericType)
                    {
                        return type;
                    }
                }
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static IList<Type> ClassesOfType<T>(bool excludeAbstract = true)
        {
            _allAssemblies = AssemblyLoader.GetAppDomainAssemblies();
            return _finder.OfType<T>(excludeAbstract);
        }

        public static IList<Type> EnumTypes()
        {
            var loadedTypes = new List<Type>();
            foreach (var assembly in _allAssemblies)
            {
                //exclude if it's a system assembly
                if (AssemblyLoader.IsSystemAssembly(assembly.FullName))
                    continue;
                try
                {
                    //if error occurs while loading the assembly, continue or throw error
                    var types = assembly.GetTypes().Where(x => x.IsEnum).ToList();
                    foreach (var type in types)
                    {
                        loadedTypes.Add(type);
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return loadedTypes;
        }
    }
}