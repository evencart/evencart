using RoastedMarketplace.Data.Enum;

namespace RoastedMarketplace.Areas.Administration.Models.Settings
{
    public class SystemSettingsModel : SettingsModel
    {
        public LogLevel MinimumLogLevel { get; set; }
    }
}