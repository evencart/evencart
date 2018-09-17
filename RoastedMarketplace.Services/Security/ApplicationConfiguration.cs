using System.Configuration;

namespace RoastedMarketplace.Services.Security
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string GetSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        public void SetSetting(string settingName, string value)
        {
            //open the configuration
            ConfigurationManager.AppSettings[settingName] = value;
        }
    }
}
