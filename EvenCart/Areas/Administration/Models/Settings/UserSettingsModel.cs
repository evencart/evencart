using System.Collections.Generic;
using EvenCart.Data.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class UserSettingsModel : SettingsModel
    {
        /// <summary>
        /// Default registration mode for users
        /// </summary>
        public RegistrationMode UserRegistrationDefaultMode { get; set; }

        /// <summary>
        /// Specifies if user names are enabled for site
        /// </summary>
        public bool AreUserNamesEnabled { get; set; }

        public int MaximumNumberOfVisibleNotifications { get; set; }

        public List<SelectListItem> AvailableUserRegistrationModes { get; set; }
    }
}