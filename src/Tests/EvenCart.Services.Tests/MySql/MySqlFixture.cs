using EvenCart.Genesis;
using EvenCart.Services.Installation;
using Genesis;
using Genesis.Database;
using Genesis.Infrastructure.IO;
using Genesis.Modules.Stores;
using Genesis.Modules.Web;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [SetUpFixture]
    public class MySqlFixture : BaseFixture
    {
        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            TestDbInit.MySql(BaseTest.MySqlConnectionString);
            var installationService = new InstallationService(TestDbInit.DbSettings, D.Resolve<ILocalFileProvider>());
            installationService.Install();
            installationService.FillRequiredSeedData("admin@store.com", "@#$%^&*", "localhost", "Test Store");
            //set the current store to 1
            GenesisEngine.Instance.CurrentHttpContext.SetCurrentStore(new Store() {Id = 1});
        }

        [OneTimeTearDown]
        public void OnetimeTeardown()
        {
            DatabaseManager.CleanupDatabase(BaseTest.ContextKey);
        }
    }
}