using EvenCart.Core.Config;

namespace EvenCart.Data.Entity.Settings
{
    public class GdprSettings : ISettingGroup
    {
        public bool UseConsentGroup { get; set; }

        public int ConsentGroupId { get; set; }

        public string CookiePopupText { get; set; }

        public bool ShowCookiePopup { get; set; }
    }
}