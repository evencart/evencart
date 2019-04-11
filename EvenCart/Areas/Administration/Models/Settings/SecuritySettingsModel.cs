using System.Collections.Generic;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class SecuritySettingsModel : FoundationModel
    {
        public SecuritySettingsModel()
        {
            AvailablePasswordStorageFormats = new List<PasswordFormat>();
        }
        /// <summary>
        /// Default password format
        /// </summary>
        public PasswordFormat DefaultPasswordStorageFormat { get; set; }

        public List<PasswordFormat> AvailablePasswordStorageFormats { get; set; }
    }
}