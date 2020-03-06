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

using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
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
      
        public int FaviconId { get; set; }

        public string StoreName { get; set; }

        public bool EnableBreadcrumbs { get; set; }

        public int PrimaryNavigationId { get; set; }

        public bool EnableJsBundling { get; set; }

        public bool EnableCssBundling { get; set; }

        public bool EnableHtmlMinification { get; set; }

        public string DefaultPageTitle { get; set; }

        public string DefaultMetaKeywords { get; set; }

        public string DefaultMetaDescription { get; set; }

        public string ActiveTheme { get; set; }
    }
}