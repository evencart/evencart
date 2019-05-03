using System.Data.SqlClient;
using System.Linq;
using DotEntity;
using DotEntity.MySql;
using DotEntity.Providers;
using DotEntity.SqlServer;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Plugins;
using EvenCart.Data.Versions;

namespace EvenCart.Data.Database
{
    public class DatabaseManager
    {
        private const string DatabaseContextKey = "EvenCart";
        private const string TablePrefix = "";

        public const string SqlServerProvider = "SqlServer";
        public const string MySqlProvider = "MySql";

        public static void InitDatabase(IDatabaseSettings dbSettings)
        {
            if (dbSettings.HasSettings())
            {
                DotEntityDb.GlobalTableNamePrefix = TablePrefix;
                DotEntityDb.Initialize(dbSettings.ConnectionString, GetProvider(dbSettings.ProviderName));
            }
        }

        public static bool IsMySqlProvider()
        {
            var dbSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            return dbSettings.ProviderName == MySqlProvider;
        }

        public static bool IsSqlServerProvider()
        {
            var dbSettings = DependencyResolver.Resolve<IDatabaseSettings>();
            return dbSettings.ProviderName == SqlServerProvider;
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
                case "mysql":
                    return new MySqlDatabaseProvider();

            }
            return null;
        }

        private static bool _versionsAdded = false;
        public static void AppendVersions(bool withPlugins = false)
        {
            if (_versionsAdded)
                return;
            DotEntityDb.EnqueueVersions(DatabaseContextKey, new Version100(), new Version101());
           
            var pluginLoader = DependencyResolver.Resolve<IPluginLoader>();
            var pluginInfos = pluginLoader.GetAvailablePlugins();
            foreach (var pluginInfo in pluginInfos)
            {
                try
                {
                    var versions = pluginInfo.LoadPluginInstance<IPlugin>().GetDatabaseVersions().ToArray();
                    if (versions.Any())
                        DotEntityDb.EnqueueVersions(pluginInfo.SystemName, versions);
                }
                catch
                {
                    // ignored
                }
            }
            _versionsAdded = true;
        }

        public static void UpgradeDatabase(bool withPlugins = true)
        {
            UpgradeDatabase(DatabaseContextKey);
            if (withPlugins)
            {
                //upgrade the installed plugin's database as well.
                var pluginLoader = DependencyResolver.Resolve<IPluginLoader>();
                var pluginInfos = pluginLoader.GetAvailablePlugins();
                foreach (var pluginInfo in pluginInfos)
                {
                    if (pluginInfo.Installed)
                    {
                        UpgradeDatabase(pluginInfo.SystemName);
                    }
                }
            }
          
        }

        public static void UpgradeDatabase(string callingContextName)
        {
            AppendVersions();
            DotEntityDb.UpdateDatabaseToLatestVersion(callingContextName);
        }

        public static void CleanupDatabase(string callingContextName)
        {
            AppendVersions();
            DotEntityDb.UpdateDatabaseToVersion(callingContextName, null);
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
        public static string CreateSqlServerConnectionString(ConnectionStringRequest request)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder {
                    IntegratedSecurity = request.IntegratedSecurity,
                    DataSource = request.ServerName,
                    InitialCatalog = request.DatabaseName
                };
                if (!request.IntegratedSecurity)
                {
                    builder.UserID = request.UserName;
                    builder.Password = request.Password;
                }
                builder.PersistSecurityInfo = false;
                if (request.Timeout > 0)
                {
                    builder.ConnectTimeout = request.Timeout;
                }
                return builder.ConnectionString;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string CreateMysqlConnectionString(ConnectionStringRequest request)
        {
            return $"Server={request.ServerName};Database={request.DatabaseName};Uid={request.UserName};Pwd={request.Password};";
        }

        public static string CreateConnectionString(ConnectionStringRequest request)
        {
            if (request.ProviderName == SqlServerProvider)
                return CreateSqlServerConnectionString(request);
            if (request.ProviderName == MySqlProvider)
                return CreateMysqlConnectionString(request);
            return null;
        }
    }
}