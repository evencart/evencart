using RoastedMarketplace.Core.Config;
using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class SecuritySettings : ISettingGroup
    {
        /// <summary>
        /// Default password format
        /// </summary>
        public PasswordFormat DefaultPasswordStorageFormat { get; set; }
    }
}