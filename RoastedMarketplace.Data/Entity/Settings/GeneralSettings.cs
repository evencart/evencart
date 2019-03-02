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

        /// <summary>
        /// The logo id of the website
        /// </summary>
        public int LogoId { get; set; }

        public string StoreName { get; set; }

        public bool EnableBreadcrumbs { get; set; }

        public int PrimaryNavigationId { get; set; }
    }
}