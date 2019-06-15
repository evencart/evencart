using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EvenCart.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace EvenCart.Core
{
    public class ServerHelper
    {
        /// <summary>
        /// Gets available timezones on the server
        /// </summary>
        public static ReadOnlyCollection<TimeZoneInfo> GetAvailableTimezones()
        {
            var availableTimezones = TimeZoneInfo.GetSystemTimeZones();
            return availableTimezones;
        }

        public static IEnumerable<CultureInfo> GetAvailableCultureInfos()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures).OrderBy(x => x.EnglishName);
        }

        public static void RestartApplication()
        {
            var applicationLifetime = DependencyResolver.Resolve<IApplicationLifetime>();
            applicationLifetime.StopApplication();
        }

        public static string MapPath(string relativePath, bool isWebRootPath = false)
        {
            if (!relativePath.StartsWith("~/"))
                return relativePath;
            var hostingEnvironment = DependencyResolver.Resolve<IHostingEnvironment>();
            return relativePath.Replace("~",
                isWebRootPath ? hostingEnvironment.WebRootPath : hostingEnvironment.ContentRootPath).Replace("/", "\\");
        }
    }
}