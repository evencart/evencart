using RoastedMarketplace.Core.Config;

namespace RoastedMarketplace.Data.Entity.Settings
{
    public class DateTimeSettings : ISettingGroup
    {
        public string DefaultTimeZoneId { get; set; }
    }
}