using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class GeneralSettings : ISettingGroup
    {
        /// <summary>
        /// The domain where public website has been installed
        /// </summary>
        public string StoreDomain { get; set; }

        /// <summary>
        /// Default timezone to be used for network
        /// </summary>
        public string DefaultTimeZoneId { get; set; }
    }
}