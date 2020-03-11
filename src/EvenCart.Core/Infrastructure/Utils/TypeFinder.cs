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
using System.Linq;
using System.Reflection;

namespace EvenCart.Core.Infrastructure.Utils
{
    public sealed class TypeFinder
    {
        private static string[] SolutionAssemblies = {"EvenCart", "EvenCart.Core", "EvenCart.Data", "EvenCart.Services", "EvenCart.Infrastructure"};

        private static TypeFinder _finder
        {
            get { return Singleton<TypeFinder>.Instance; }
        }


        private static IList<Assembly> _allAssemblies;

        private IList<Type> _allTypes;
        IList<Type> AllTypes()
        {
            if (_allTypes != null)
                return _allTypes;

            var loadedTypes = new List<Type>();
            foreach (var assembly in _allAssemblies)
            {
                //exclude if it's a system assembly
                if (AssemblyLoader.IsSystemAssembly(assembly.FullName))
                    continue;
                try
                {
                    //if error occurs while loading the assembly, continue or throw error
                    var types = assembly.GetTypes();
                    loadedTypes = loadedTypes.Concat(types).ToList();
                }
                catch
                {
                    // ignored
                }
            }

            _allTypes = loadedTypes;
            return loadedTypes;
        }

        IList<Type> OfType<T>(bool excludeAbstract = true, bool restrictToSolutionAssemblies = false)
        {
            var loadedTypes = new List<Type>();
            var assembliesToSearch = restrictToSolutionAssemblies
                ? _allAssemblies.Where(x => SolutionAssemblies.Contains(x.GetName().Name))
                : _allAssemblies;
            foreach (var assembly in assembliesToSearch)
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

        IList<Type> GetByNamesImpl(IList<string> typeNames)
        {
            var allTypes = AllTypes().Where(x => typeNames.Contains(x.FullName)).ToList();
            return allTypes;
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

        public static IEnumerable<Type> ClassesOfType<T>(Assembly assembly, bool excludeAbstract = true)
        {
            //exclude if it's a system assembly
            if (AssemblyLoader.IsSystemAssembly(assembly.FullName))
                yield return null;
            //if error occurs while loading the assembly, continue or throw error
            var types = assembly.GetTypes().Where(x => x.IsClass).ToList();
            foreach (var type in types)
            {
                if (excludeAbstract && type.IsClass && type.IsAbstract)
                    continue;

                if (typeof(T).IsAssignableFrom(type) || typeof(T).IsGenericType)
                {
                    yield return type;
                }
            }
        }

        public static IList<Type> ClassesOfType<T>(bool excludeAbstract = true, bool restrictToSolutionAssemblies = false)
        {
            _allAssemblies = _allAssemblies ?? AssemblyLoader.GetAppDomainAssemblies();
            return _finder.OfType<T>(excludeAbstract, restrictToSolutionAssemblies);
        }

        public static IList<T> InstancesOfType<T>() where T : class
        {
            return ClassesOfType<T>()
                .Select(x => (T) (DependencyResolver.ResolveOptional(x) ?? Activator.CreateInstance(x)))
                .ToList();
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

        public static IList<Type> GetByNames(IList<string> typeNames)
        {
            return _finder.GetByNamesImpl(typeNames);
        }
    }
}