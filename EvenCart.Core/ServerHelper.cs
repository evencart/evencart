using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

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
    }
}