using System;
using System.Collections.ObjectModel;

namespace RoastedMarketplace.Core
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
    }
}