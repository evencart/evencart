using DotEntity;
using DotEntity.SqlServer;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Services.Installation;

namespace RoastedMarketplace.Services.Tests
{
    public class TestDbInit
    {
        public static void SqlServer(string connectionString)
        {
            DotEntityDb.Initialize(connectionString, new SqlServerDatabaseProvider());
            DatabaseManager.ClearVersions();
            DatabaseManager.UpgradeDatabase();

            //seed data
            var installationService = DependencyResolver.Resolve<IInstallationService>();
            installationService.FillRequiredSeedData("admin@store.com", "@#$%^&*", "localhost");
        }
    }
}