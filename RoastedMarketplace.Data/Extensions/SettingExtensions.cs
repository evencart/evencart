using RoastedMarketplace.Data.Entity.Settings;

namespace RoastedMarketplace.Data.Extensions
{
    public static class SettingExtensions
    {
        public static bool GetBoolean(this Setting setting)
        {
            return setting.Value.GetBoolean();
        }

       
    }
}