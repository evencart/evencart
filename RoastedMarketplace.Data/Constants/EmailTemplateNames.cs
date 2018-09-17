namespace RoastedMarketplace.Data.Constants
{
    public class EmailTemplateNames
    {
        const string AdminSuffix = ".Admin";

        public const string Master = "Master";

        public const string UserRegisteredMessage = "User.Registered";

        public const string UserRegisteredMessageToAdmin = UserRegisteredMessage + AdminSuffix;

        public const string UserActivatedMessage = "User.Activated";

        public const string UserActivationLinkMessage = "User.ActivationLink";

        public const string PasswordRecoveryLinkMessage = "Common.PasswordRecovery";

        public const string PasswordChangedMessage = "Common.PasswordChanged";

        public const string UserDeactivatedMessage = "User.Deactivated";

        public const string UserDeactivatedMessageToAdmin = UserDeactivatedMessage + AdminSuffix;

        public const string UserAccountDeletedMessage = "User.AccountDeleted";

        public const string UserAccountDeletedMessageToAdmin = UserAccountDeletedMessage + AdminSuffix;

    }
}
