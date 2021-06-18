#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using Genesis.Config;
using Microsoft.Extensions.Configuration;

namespace EvenCart.Infrastructure.Config
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
