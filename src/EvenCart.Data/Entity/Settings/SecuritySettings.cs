using EvenCart.Core.Config;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Settings
{
    public class SecuritySettings : ISettingGroup
    {
        /// <summary>
        /// Default password format
        /// </summary>
        public PasswordFormat DefaultPasswordStorageFormat { get; set; }

        public int ExpirePasswordDays { get; set; }

        public int EmailVerificationLinkExpirationHours { get; set; }

        public int PasswordResetLinkExpirationHours { get; set; }

        public bool EnableCaptcha { get; set; }

        public string BannedIps { get; set; }

        public string AdminRestrictedIps { get; set; }
    }
}