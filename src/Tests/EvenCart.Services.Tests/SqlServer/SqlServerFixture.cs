using EvenCart.Data.Database;
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
            var installationService = new InstallationService(TestDbInit.DbSettings);
            installationService.Install();
            installationService.FillRequiredSeedData("admin@store.com", "@#$%^&*", "localhost", "Test Store");
        }

        [OneTimeTearDown]
        public void OnetimeTeardown()
        {
            DatabaseManager.CleanupDatabase(BaseTest.ContextKey);
        }
    }
}