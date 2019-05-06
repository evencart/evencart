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
    }
}