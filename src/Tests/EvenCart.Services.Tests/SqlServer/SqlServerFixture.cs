using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Services.Installation;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [SetUpFixture]
    public class SqlServerFixture : BaseFixture
    {
        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            TestDbInit.SqlServer(BaseTest.MsSqlConnectionString);
            var installationService = new InstallationService(TestDbInit.DbSettings, DependencyResolver.Resolve<ILocalFileProvider>());
            installationService.Install();
            installationService.FillRequiredSeedData("admin@store.com", "@#$%^&*", "localhost", "Test Store");

            //set the current store to 1
            ApplicationEngine.CurrentHttpContext.SetCurrentStore(new Store() { Id = 1 });
        }

        [OneTimeTearDown]
        public void OnetimeTeardown()
        {
            DatabaseManager.CleanupDatabase(BaseTest.ContextKey);
        }
    }
}