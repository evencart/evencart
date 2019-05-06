using EvenCart.Data.Enum;

namespace EvenCart.Areas.Administration.Models.Settings
{
    public class SystemSettingsModel : SettingsModel
    {
        public LogLevel MinimumLogLevel { get; set; }
    }
}