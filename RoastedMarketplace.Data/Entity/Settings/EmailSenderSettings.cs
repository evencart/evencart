using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class EmailSenderSettings : ISettingGroup
    {
        public bool UserRegisteredEmailEnabled { get; set; }

        public bool UserRegisteredEmailToAdminEnabled { get; set; }

        public bool UserActivationEmailEnabled { get; set; }

        public bool PasswordChangedEmailEnabled { get; set; }

        public bool UserDeactivationEmailEnabled { get; set; }

        public bool UserDeactivationEmailToAdminEnabled { get; set; }

        public bool UserDeletedEmailEnabled { get; set; }

        public bool UserDeletedEmailToAdminEnabled { get; set; }
    }
}