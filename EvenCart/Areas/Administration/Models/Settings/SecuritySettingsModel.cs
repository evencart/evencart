namespace EvenCart.Areas.Administration.Models.Settings
{
    public class SecuritySettingsModel : SettingsModel
    {
        public bool EnableCaptcha { get; set; }

        public string BannedIps { get; set; }

        public string AdminRestrictedIps { get; set; }
    }
}