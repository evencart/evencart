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

        /// <summary>
        /// Specifies if user logging in with connected account be immediately activated
        /// </summary>
        public bool ActivateUserForConnectedAccount { get; set; }

        /// <summary>
        /// Specifies if activation code should be a 6 digit numeric code instead of alphanumeric strings
        /// </summary>
        public bool UseNumericCodeForActivationEmail { get; set; }
    }
}