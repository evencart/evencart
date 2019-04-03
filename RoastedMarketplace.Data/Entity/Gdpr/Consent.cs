using RoastedMarketplace.Core.Data;

namespace RoastedMarketplace.Data.Entity.Gdpr
{
    public class Consent : FoundationEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPluginSpecificConsent { get; set; }

        public string PluginSystemName { get; set; }

        public bool IsRequired { get; set; }

        public int DisplayOrder { get; set; }

        public string LanguageCultureCode { get; set; }

        public bool EnableLogging { get; set; }

        public bool OneTimeSelection { get; set; }

        public bool Published { get; set; }

        public int ConsentGroupId { get; set; }

        #region Virtual Properties
        public virtual ConsentGroup ConsentGroup { get; set; }
        #endregion
    }
}