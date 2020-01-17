using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Settings;

namespace EvenCart.Data.Extensions
{
    public static class SettingsExtensions
    {
        public static string GetUrlProtocol(this UrlSettings urlSettings)
        {
            return urlSettings.EnableSsl ? "https" : "http";
        }

        public static IList<string> GetBannedIps(this SecuritySettings securitySettings)
        {
            return securitySettings.BannedIps?.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        public static IList<string> GetAdminRestrictedIps(this SecuritySettings securitySettings)
        {
            return securitySettings.AdminRestrictedIps?.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        public static decimal GetAffiliateAmount(this AffiliateSettings affiliateSettings, decimal amount)
        {
            return affiliateSettings.UseCommissionPercentage
                ? (amount * affiliateSettings.CommissionValue) / 100
                : affiliateSettings.CommissionValue;
        }
    }
}