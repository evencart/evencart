using EvenCart.Core.Config;
using EvenCart.Data.Enum;

namespace EvenCart.Data.Entity.Settings
{
    public class UserSettings: ISettingGroup
    {
        /// <summary>
        /// Default registration mode for users
        /// </summary>
        public RegistrationMode UserRegistrationDefaultMode { get; set; }

        /// <summary>
        /// Specifies if user names are enabled for site
        /// </summary>
        public bool AreUserNamesEnabled { get; set; }

        /// <summary>
        /// Specifies if profile pictures are enabled
        /// </summary>
        public bool AreProfilePicturesEnabled { get; set; }
    }
}