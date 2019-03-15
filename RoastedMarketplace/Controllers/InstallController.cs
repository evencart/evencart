using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Models.Installation;
using RoastedMarketplace.Services.Installation;

namespace RoastedMarketplace.Controllers
{
    public class InstallController : FoundationController
    {
        private readonly IInstallationService _installationService;
        public InstallController(IInstallationService installationService)
        {
            _installationService = installationService;
        }

        [Route("install")]
        [HttpPost]
        [ValidateModelState(ModelType = typeof(InstallationRequestModel))]
        public IActionResult Install(InstallationRequestModel model)
        {
            var databaseSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            var areTableInstalled = DatabaseManager.IsDatabaseInstalled(databaseSettings);

            if (areTableInstalled)
                return R.Fail.With("error", T("Database already installed")).Result;

            //lets save the database settings to config file
            var connectionString = model.ConnectionString;
            var providerName = "SqlServer"; //todo: make it selectable to allow sqlite and other providers
          
            //create the connection string if required
            if (!model.IsConnectionString)
            {
                connectionString = DatabaseManager.CreateSqlServerConnectionString(model.ServerUrl, model.DatabaseName,
                    model.DatabaseUserName, model.DatabasePassword, false, 0);
            }

            //check if we have correct connection string
            if (!DatabaseManager.IsValidConnection(providerName, connectionString))
            {
                return R.Fail.With("error", T("Failed to connect to database")).Result;
            }

            databaseSettings.WriteSettings(connectionString, providerName);

            //perform the installation
            _installationService.Install();

            //then feed the data
            _installationService.FillRequiredSeedData(model.AdminEmail, model.Password,
                ApplicationEngine.CurrentHttpContext.Request.Host.Value, model.StoreName);

            return R.Success.Result;
        }
    }
}