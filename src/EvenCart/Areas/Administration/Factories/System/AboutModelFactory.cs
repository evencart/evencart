using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using EvenCart.Areas.Administration.Models.System;
using EvenCart.Core.Infrastructure.Utils;

namespace EvenCart.Areas.Administration.Factories.System
{
    public class AboutModelFactory : IAboutModelFactory
    {
        public AboutModel Create()
        {
            var utcNow = DateTime.UtcNow;
            var model = new AboutModel
            {
                OperatingSystemName = Environment.OSVersion.VersionString,
                FrameworkVersion = RuntimeInformation.FrameworkDescription,
                AspNetVersion = RuntimeEnvironment.GetSystemVersion(),
                TimeZone = TimeZoneInfo.Local.StandardName,
                UtcTime = $"{utcNow.ToLongDateString()} {utcNow.ToLongTimeString()}",
                LoadedAssemblies = new List<string>()
            };

            //get the loaded assemblies
            var assemblies = AssemblyLoader.GetAppDomainAssemblies();
            foreach (var asm in assemblies)
            {
                model.LoadedAssemblies.Add(asm.FullName);
            }

            model.LoadedAssemblies = model.LoadedAssemblies.OrderBy(x => x).ToList();
            return model;
        }
    }
}