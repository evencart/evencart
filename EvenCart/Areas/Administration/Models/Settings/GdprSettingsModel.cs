namespace EvenCart.Areas.Administration.Models.Settings
{
    public class GdprSettingsModel : SettingsModel
    {
        public bool UseConsentGroup { get; set; }

        public int ConsentGroupId { get; set; }

        public string CookiePopupText { get; set; }

        public bool ShowCookiePopup { get; set; }
    }
}