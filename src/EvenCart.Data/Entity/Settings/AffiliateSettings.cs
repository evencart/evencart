using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class AffiliateSettings : ISettingGroup
    {
        public bool EnableAffiliates { get; set; }

        public bool UseCommissionPercentage { get; set; }

        public decimal CommissionValue { get; set; }

        public decimal SignupCreditToAffiliate { get; set; }

        public decimal SignupCreditToNewUser { get; set; }

        public bool ExcludeTaxFromCalculation { get; set; }

        public string AffiliateCookieName { get; set; }

        public int AffiliateCookieExpirationDays { get; set; }

        public decimal StoreCreditsExchangeRate { get; set; }

        public bool AutoActivateAffiliateAccount { get; set; }

        public bool AllowStoreCreditsForPurchases { get; set; }

        public decimal MinimumStoreCreditsToAllowPurchases { get; set; }
    }
}