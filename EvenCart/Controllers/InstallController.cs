using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Services.Installation;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Installation;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    public class InstallController : FoundationController
    {
        private readonly IInstallationService _installationService;
        public InstallController(IInstallationService installationService)
        {
            _installationService = installationService;
        }

        [HttpGet("install", Name = RouteNames.Install)]
        public IActionResult Index()
        {
            var databaseSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            var areTableInstalled = DatabaseManager.IsDatabaseInstalled(databaseSettings);

            if (areTableInstalled)
                return RedirectToRoute(RouteNames.Home);
            return R.Success.Result;
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
            var providerName = "SqlServer"; //todo: make it selectable to allow sqlite and other providers

            //create the connection string if required
            if (!model.IsConnectionString)
            {
                connectionString = DatabaseManager.CreateSqlServerConnectionString(model.ServerUrl, model.DatabaseName,
                    model.DatabaseUserName, model.DatabasePassword, model.IntegratedSecurity, 0);
            }

            //check if we have correct connection string
            if (!DatabaseManager.IsValidConnection(providerName, connectionString))
            {
                return Json(new { success = false, error = T("Failed to connect to database") });
            }

            databaseSettings.WriteSettings(connectionString, providerName);

            //perform the installation
            _installationService.Install();

            //then feed the data
            _installationService.FillRequiredSeedData(model.AdminEmail, model.Password,
                ApplicationEngine.CurrentHttpContext.Request.Host.Value, model.StoreName);

            //restart the app
            ServerHelper.RestartApplication();

            return Json(new { success = true });
        }

        [HttpPost("test-connection", Name = RouteNames.TestDatabaseConnection)]
        public IActionResult TestConnection(TestConnectionModel model)
        {
            //lets save the database settings to config file
            var connectionString = model.ConnectionString;
            var providerName = "SqlServer"; //todo: make it selectable to allow sqlite and other providers

            //create the connection string if required
            if (!model.IsConnectionString)
            {
                connectionString = DatabaseManager.CreateSqlServerConnectionString(model.ServerUrl, model.DatabaseName,
                    model.DatabaseUserName, model.DatabasePassword, model.IntegratedSecurity, 0);
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