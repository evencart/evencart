namespace EvenCart.Areas.Administration.Models.Settings
{
    public class SecuritySettingsModel : SettingsModel
    {
        public bool EnableCaptcha { get; set; }
       
        public string SiteKey { get; set; }
     
        public string SiteSecret { get; set; }

        public string BannedIps { get; set; }

        public string AdminRestrictedIps { get; set; }

        public string SharedVerificationKey { get; set; }

        public string HoneypotFieldName { get; set; }
    }
}