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

        public static string MapPath(string relativePath, bool isWebRootPath = false, IHostingEnvironment hostingEnv = null)
        {
            if (!relativePath.StartsWith("~/"))
                return relativePath;
            var hostingEnvironment = hostingEnv ?? DependencyResolver.Resolve<IHostingEnvironment>();
            return relativePath.Replace("~",
                isWebRootPath ? hostingEnvironment.WebRootPath : hostingEnvironment.ContentRootPath); //.Replace("/", "\\");
        }
    }
}