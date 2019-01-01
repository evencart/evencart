namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class GeneralSettingsModel : SettingsModel
    {
        public string StoreDomain { get; set; }

        public string DefaultTimeZoneId { get; set; }
    }
}