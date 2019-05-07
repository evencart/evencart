using EvenCart.Data.Enum;

namespace EvenCart.Infrastructure
{
    public static class ApplicationConfig
    {
        public const string Version = "1.0.0";

        public const string ApiVersion = "1.0";

        public const string ApiEndpointName = "api";

        public const string DefaultAuthenticationScheme = "AppAuthentication";

        public const string VisitorAuthenticationScheme = "VisitorAuthentication";

        public const string ImitatorKey = "imitator";

        public const string PersistanceKey = "ispersistant";

        public const string ApiAuthenticationScheme = "ApiAuthentication";

        public const string ExternalAuthenticationScheme = "ExternalAuthentication";

        public const string DefaultLoginUrl = "/login";

        public const string GlobalLanguagesDirectory = "~/App_Data/Languages";

        public const string FlagsDirectory = "~/common/flags";

        public const string BundlesDirectory = "~/bundles";

        public const string MediaDirectory = "~/Content/Uploads";

        public const string MediaServeDirectory = "~/Content/Uploads/Serves";

        public const string DefaultImagePath = "~/Content/Uploads/_Default.jpg";

        public const string AdminAreaName = "admin";

        public const int AdminThumbnailWidth = 100;

        public const int AdminThumbnailHeight = 100;

        public const string PrimaryNavigationName = "primary_navigation";

        public const string SecondaryNavigationName = "secondary_navigation";

        public const PasswordFormat DefaultPasswordFormat = PasswordFormat.Sha256Hashed;

        public const string ConsentCookieName = "_gdprc";

        public const string AppSettingsEncryptionKey = "encryptionKey";

        public const string AppSettingsEncryptionSalt = "encryptionSalt";

        public const string AppSettingsApiSecret = "apiSecret";

        public const string DefaultLogoUrl = "~/common/assets/images/logo.png";

        public const string DefaultFaviconUrl = "~/common/assets/images/favicon.png";

    }
}