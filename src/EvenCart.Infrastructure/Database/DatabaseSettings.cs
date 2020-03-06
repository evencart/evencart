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

using System;
using System.Collections.Generic;
using System.IO;
using EvenCart.Core;
using EvenCart.Core.Services.Configuration;
using EvenCart.Data.Database;
using EvenCart.Services.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace EvenCart.Infrastructure.Database
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; private set; }

        public string ProviderName { get; private set; }

        public IConfigurationFileService ConfigurationFileService { get; } = new XmlConfigurationFileService();

        private string _saveFileName;
        public DatabaseSettings(IHostingEnvironment hostingEnvironment)
        {
            _saveFileName = ServerHelper.MapPath("~/App_Data/database.config", hostingEnv: hostingEnvironment);
            LoadSettings();
        }

        
        private bool _hasSettings = false;
        public void LoadSettings()
        {
            
            if (!File.Exists(_saveFileName))
                return;

            var configFileService = ConfigurationFileService;

            var configValues = configFileService.ReadFile(_saveFileName);
            if(configValues == null)
                throw new Exception("Invalid configuration file");

            ConnectionString = configValues["ConnectionString"];
            ProviderName = configValues["ProviderName"];
            _hasSettings = true;

        }
        
        public void WriteSettings(string connectionString, string providerName)
        {
            var configFileService = ConfigurationFileService;
            var configValues = new Dictionary<string, string>()
            {
                {"ConnectionString", connectionString},
                {"ProviderName", providerName}
            };
            //write settings
            configFileService.WriteFile(_saveFileName, configValues);

            ConnectionString = connectionString;
            ProviderName = providerName;
            _hasSettings = true;
        }

        public bool HasSettings()
        {
            return _hasSettings;
        }
    }
}