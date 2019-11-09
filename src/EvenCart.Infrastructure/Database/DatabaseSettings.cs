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