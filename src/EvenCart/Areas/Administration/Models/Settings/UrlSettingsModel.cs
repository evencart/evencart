namespace EvenCart.Areas.Administration.Models.Settings
{
    public class UrlSettingsModel : SettingsModel
    {
        public bool EnableSsl { get; set; }

        public string ProductUrlTemplate { get; set; }

        public string CategoryUrlTemplate { get; set; }

        public string ContentPageUrlTemplate { get; set; }
    }
}