using System.Security.Policy;
using EvenCart.Data.Entity.Settings;

namespace EvenCart.Data.Extensions
{
    public static class SettingsExtensions
    {
        public static string GetUrlProtocol(this UrlSettings urlSettings)
        {
            return urlSettings.EnableSsl ? "https" : "http";
        }
    }
}