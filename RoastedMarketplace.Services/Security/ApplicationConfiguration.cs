using Microsoft.Extensions.Configuration;

namespace RoastedMarketplace.Services.Security
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly IConfiguration _configuration;
        public ApplicationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSetting(string settingName)
        {
            return _configuration[settingName];
        }

        public void SetSetting(string settingName, string value)
        {
            //open the configuration
            _configuration[settingName] = value;
        }
    }
}
