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

using Genesis.Modules.Users;

namespace Genesis
{
    public class StaticConfig : IApplicationConfig
    {
        public  string AppName => "EvenCart";

        public  string ApiVersion => "1.0";

        public  string ApiEndpointName => "api";

        public  string DefaultAuthenticationScheme => "AppAuthentication";

        public  string VisitorAuthenticationScheme => "VisitorAuthentication";

        public  string ImitatorKey => "imitator";

        public  string PersistanceKey => "ispersistant";

        public  string ApiAuthenticationScheme => "ApiAuthentication";

        public  string ExternalAuthenticationScheme => "ExternalAuthentication";

        public  string DefaultLoginUrl => "/login";

        public  string GlobalLanguagesDirectory => "~/App_Data/Languages";

        public  string SecureDataDirectory => "App_Data/Security";

        public  string FlagsDirectory => "~/common/flags";

        public  string BundlesDirectory => "~/bundles";

        public  string MediaDirectory => "~/Content/Uploads";

        public  string MediaServeDirectory => "~/Content/Uploads/Serves";

        public  string DefaultImagePath => "~/Content/Uploads/_Default.jpg";

        public  string ThemeDirectory => "~/Content/Themes";

        public  string AdminAreaName => "admin";

        public  int AdminThumbnailWidth => 100;

        public  int AdminThumbnailHeight => 100;

        public  string PrimaryNavigationName => "primary_navigation";

        public  string SecondaryNavigationName => "secondary_navigation";

        public  PasswordFormat DefaultPasswordFormat => PasswordFormat.Sha256Hashed;

        public  string ConsentCookieName => "_gdprc";

        public  string AppSettingsEncryptionKey => "encryptionKey";

        public  string AppSettingsEncryptionSalt => "encryptionSalt";

        public  string AppSettingsApiSecret => "apiSecret";

        public  string AppSettingsCacheProvider => "cacheProvider";

        public  string DefaultLogoUrl => "~/common/assets/images/logo.png";

        public  string DefaultFaviconUrl => "~/common/assets/images/favicon.png";

        public  string UnavailableMethodName => "Not Available";

        public  string NewsletterSubscriptionGuid => "E0D79E93-03AC-43B8-8D35-E8FE447244FA";

        public  string InbuiltWidgetPluginName => "EvenCart.InbuiltWidgets";

        public  string TestEnvironmentName => "EvenCart.TestEnvironment";

        public  string AffiliateIdQueryStringParameterName => "via";

        public  string StoreCreditPaymentMethodName => "StoreCredit";

    }
}