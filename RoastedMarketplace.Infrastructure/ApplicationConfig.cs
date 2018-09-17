namespace RoastedMarketplace.Infrastructure
{
    public static class ApplicationConfig
    {
        public const string ApiEndpointName = "api";

        public const string DefaultAuthenticationScheme = "AppAuthentication";

        public const string ApiAuthenticationScheme = "ApiAuthentication";

        public const string ExternalAuthenticationScheme = "External";

        public const string DefaultLoginUrl = "/Authentication/Login";

        public const string GlobalLanguagesDirectory = "~/App_Data/Languages";

        public const string AdminAreaName = "admin";
    }
}