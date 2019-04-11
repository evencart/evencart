using System.ComponentModel.DataAnnotations;
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Settings
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