using System;
using EvenCart.Core.Config;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Settings
{
    public class SystemSettings : ISettingGroup
    {
        /// <summary>
        /// Is the application installed?
        /// </summary>
        public bool IsInstalled { get; set; }

        /// <summary>
        /// Specifies minimum log level that should be used for logging
        /// </summary>
        public LogLevel MinimumLogLevel { get; set; }

        /// <summary>
        /// Specifies the update fetch interval in hours.
        /// </summary>
        public int UpdateFetchIntervalInHours { get; set; }

        /// <summary>
        /// The latest updates fetched
        /// </summary>
        public string LatestUpdatesFetched { get; set; }

        /// <summary>
        /// The latest date when updates were fetched
        /// </summary>
        public DateTime LatestFetchedOn { get; set; }
    }
}