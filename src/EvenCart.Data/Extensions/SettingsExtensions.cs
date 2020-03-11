#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

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