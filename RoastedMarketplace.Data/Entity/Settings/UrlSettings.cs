using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class UrlSettings : ISettingGroup
    {
        /// <summary>
        /// The activation page url that's included in the emails sent for user activation
        /// </summary>
        public string ActivationPageUrl { get; set; }

        public string ProductUrlTemplate { get; set; }

        public string TopicUrlTemplate { get; set; }

        public string CategoryUrlTemplate { get; set; }
    }
}
