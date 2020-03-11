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

using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Database;
using EvenCart.Services.Installation;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Installation;
using EvenCart.Services.Security;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class InstallController : FoundationController
    {
        private readonly IInstallationService _installationService;
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly ICryptographyService _cryptographyService;
        private readonly ILocalFileProvider _localFileProvider;
        public InstallController(IInstallationService installationService, IApplicationConfiguration applicationConfiguration, ICryptographyService cryptographyService, ILocalFileProvider localFileProvider)
        {
            _installationService = installationService;
            _applicationConfiguration = applicationConfiguration;
            _cryptographyService = cryptographyService;
            _localFileProvider = localFileProvider;
        }

        [HttpGet("install", Name = RouteNames.Install)]
        public IActionResult Index()
        {
            var databaseSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            var areTableInstalled = DatabaseManager.IsDatabaseInstalled(databaseSettings);

            if (areTableInstalled)
                return RedirectToRoute(RouteNames.Home);

            //read the license
            var license = _localFileProvider.ReadAllText(ServerHelper.MapPath("~/App_Data/Install/license.txt"));
            return R.Success.With("license", license).Result;
        }

        [HttpPost("install", Name = RouteNames.Install)]
        [ValidateModelState(ModelType = typeof(InstallationRequestModel))]
        public IActionResult Install(InstallationRequestModel model)
        {
            var databaseSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            var areTableInstalled = DatabaseManager.IsDatabaseInstalled(databaseSettings);

            if (areTableInstalled)
                return Json(new { success = false, error = T("Database already installed") });

            //lets save the database settings to config file
            var connectionString = model.ConnectionString;
            var providerName = model.ProviderName; // "SqlServer"; //todo: make it selectable to allow sqlite and other providers

            //create the connection string if required
            if (!model.IsConnectionString)
            {
                connectionString = DatabaseManager.CreateConnectionString(new ConnectionStringRequest()
                {
                    IntegratedSecurity = model.IntegratedSecurity,
                    Timeout = 0,
                    ProviderName = model.ProviderName,
                    Password = model.DatabasePassword,
                    ServerName = model.ServerUrl,
                    UserName = model.DatabaseUserName,
                    DatabaseName = model.DatabaseName
                });
            }

            //check if we have correct connection string
            if (!DatabaseManager.IsValidConnection(providerName, connectionString))
            {
                return Json(new { success = false, error = T("Failed to connect to database") });
            }

            databaseSettings.WriteSettings(connectionString, providerName);

            //perform the installation
            _installationService.Install();

            //save app settings
            _applicationConfiguration.SetSetting(ApplicationConfig.AppSettingsEncryptionKey, _cryptographyService.GetRandomPassword(32));
            _applicationConfiguration.SetSetting(ApplicationConfig.AppSettingsEncryptionSalt, _cryptographyService.GetRandomPassword(32));
            _applicationConfiguration.SetSetting(ApplicationConfig.AppSettingsApiSecret, _cryptographyService.GetRandomPassword(32));

            //then feed the data
            _installationService.FillRequiredSeedData(model.AdminEmail, model.Password,
                "//" + ApplicationEngine.CurrentHttpContext.Request.Host.Value, model.StoreName);

            //restart the app
            ServerHelper.RestartApplication();

            return Json(new { success = true });
        }

        [HttpPost("test-connection", Name = RouteNames.TestDatabaseConnection)]
        public IActionResult TestConnection(TestConnectionModel model)
        {
            //lets save the database settings to config file
            var connectionString = model.ConnectionString;
            var providerName = model.ProviderName;

            //create the connection string if required
            if (!model.IsConnectionString)
            {
                connectionString = DatabaseManager.CreateConnectionString(new ConnectionStringRequest()
                {
                    IntegratedSecurity = model.IntegratedSecurity,
                    Timeout = 0,
                    ProviderName = model.ProviderName,
                    Password = model.DatabasePassword,
                    ServerName = model.ServerUrl,
                    UserName = model.DatabaseUserName,
                    DatabaseName = model.DatabaseName
                });
            }

            //check if we have correct connection string
            if (!DatabaseManager.IsValidConnection(providerName, connectionString))
            {
                return Json(new { success = false, error = T("Failed to connect to database") });
            }

            return Json(new { success = true });
        }
    }
}