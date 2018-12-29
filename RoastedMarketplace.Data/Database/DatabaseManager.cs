using System.Data.SqlClient;
using DotEntity;
using DotEntity.Providers;
using DotEntity.SqlServer;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Versions;
using DotEntity = DotEntity.DotEntity;

namespace RoastedMarketplace.Data.Database
{
    public class DatabaseManager
    {
        private const string DatabaseContextKey = "RoastedMarketplace";
        public static void InitDatabase(IDatabaseSettings dbSettings)
        {
            if (dbSettings.HasSettings())
                DotEntityDb.Initialize(dbSettings.ConnectionString, GetProvider(dbSettings.ProviderName));
        }

        public static bool IsDatabaseInstalled()
        {
            return IsDatabaseInstalled(DependencyResolver.Resolve<IDatabaseSettings>());
        }

        public static bool IsDatabaseInstalled(IDatabaseSettings dbSettings)
        {
            return dbSettings.HasSettings() && !string.IsNullOrEmpty(dbSettings.ConnectionString) && !string.IsNullOrEmpty(dbSettings.ProviderName);
        }

        private static IDatabaseProvider GetProvider(string providerAbstractName)
        {
            switch (providerAbstractName.ToLower())
            {

                case "sqlserver":
                    return new SqlServerDatabaseProvider();
             
            }
            return null;
        }

        private static bool _versionsAdded = false;
        public static void AppendVersions()
        {
            if (_versionsAdded)
                return;
            DotEntityDb.EnqueueVersions(DatabaseContextKey, new Version100(), new Version101());
            _versionsAdded = true;
        }

        public static void UpgradeDatabase()
        {
            AppendVersions();
            DotEntityDb.UpdateDatabaseToLatestVersion(DatabaseContextKey);
        }

        public static void CleanupDatabase()
        {
            AppendVersions();
            DotEntityDb.UpdateDatabaseToVersion(DatabaseContextKey, null);
        }

        public static void ClearVersions()
        {
            _versionsAdded = false;
        }

        public static bool IsValidConnection(string providerName, string connectionString)
        {
            DotEntityDb.Initialize(connectionString, GetProvider(providerName));
            try
            {
                DotEntityDb.Provider.Connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                DotEntityDb.Provider.Connection.Close();
            }
        }
        /// <summary>
        /// Creates connection string from the provider values
        /// </summary>
        public static string CreateSqlServerConnectionString(string server, string databaseName, string userName, string password, bool integratedSecurity, int timeOut)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder {
                    IntegratedSecurity = integratedSecurity,
                    DataSource = server,
                    InitialCatalog = databaseName
                };
                if (!integratedSecurity)
                {
                    builder.UserID = userName;
                    builder.Password = password;
                }
                builder.PersistSecurityInfo = false;
                if (timeOut > 0)
                {
                    builder.ConnectTimeout = timeOut;
                }
                return builder.ConnectionString;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}