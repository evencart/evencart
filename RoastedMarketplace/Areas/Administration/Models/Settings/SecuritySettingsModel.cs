using System.Collections.Generic;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
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