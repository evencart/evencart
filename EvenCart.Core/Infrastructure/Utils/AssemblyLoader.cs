using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EvenCart.Core.Infrastructure.Utils
{
    public class AssemblyLoader
    {

        public static List<Assembly> GetAppDomainAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }

        public static Assembly LoadAssembly(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        public static List<Assembly> LoadDirectoryAssemblies(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return null;

            var allDlls = Directory.GetFiles(directoryPath, "*.dll");

            //to make sure that we don't load an already loaded assembly, let's first get the loaded assemblies
            var loadedAssemblies = GetAppDomainAssemblies();
            var loadAssemblyNames = loadedAssemblies.Select(assembly => assembly.FullName).ToList();
            foreach (var dll in allDlls)
            {
                try
                {
                    var assembly = AssemblyName.GetAssemblyName(dll);
                    var assemblyName = assembly.FullName;
                    var shouldAdd = !IsSystemAssembly(assemblyName) && !loadAssemblyNames.Contains(assemblyName);

                    if (shouldAdd)
                    {
                        AppDomain.CurrentDomain.Load(assembly);
                        loadAssemblyNames.Add(assemblyName);
                    }
                }
                catch (BadImageFormatException)
                {
                    continue;
                }
            }
            return GetAppDomainAssemblies();
        } 

        public static bool IsSystemAssembly(string assemblyName)
        {
            return Regex.IsMatch(assemblyName, @"^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^DryIoc|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^Org.Mentalis|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^TestDriven|^TestFu|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            
        }
    }
}