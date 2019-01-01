using System.ComponentModel.DataAnnotations;
using RoastedMarketplace.Infrastructure.Mvc.Models;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class SettingEntityModel : FoundationEntityModel
    {
        [Required]
        public string GroupName { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}