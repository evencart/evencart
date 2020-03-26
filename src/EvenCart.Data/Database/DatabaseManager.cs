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

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DotEntity;
using DotEntity.MySql;
using DotEntity.Providers;
using DotEntity.SqlServer;
using DotEntity.Versioning;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Plugins;
using EvenCart.Data.Extensions;
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
            return dbSettings != null && dbSettings.HasSettings() && !string.IsNullOrEmpty(dbSettings.ConnectionString) && !string.IsNullOrEmpty(dbSettings.ProviderName);
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
        public static void AppendVersions(bool excludePlugins = false)
        {
            if (_versionsAdded)
                return;
            IDatabaseVersion[] appVersions = {
                new Version1(),
                new Version1A(),
                new Version1B(),
                new Version1C(),
                new Version1D(),
                new Version1E(),
                new Version1F(),
                new Version1G(),
            };
            DotEntityDb.EnqueueVersions(DatabaseContextKey, appVersions);
            if (!excludePlugins)
            {
                //the plugin versions
                var pluginInfos = PluginLoader.GetAvailablePlugins();
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

            }

            _versionsAdded = true;
        }

        public static void UpgradeDatabase(bool excludePlugins = false)
        {
            UpgradeDatabase(DatabaseContextKey, excludePlugins);
        }

        public static void UpgradeDatabase(string callingContextName, bool excludePlugins = false)
        {
            AppendVersions(excludePlugins);
            DotEntityDb.UpdateDatabaseToLatestVersion(callingContextName);
        }

        public static void CleanupDatabase(string callingContextName)
        {
            AppendVersions();
            DotEntityDb.UpdateDatabaseToVersion(callingContextName, null);
        }

        public static IList<string> GetInstalledVersions(string callingContextName)
        {
            if (callingContextName.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException(nameof(callingContextName));
            return DotEntityDb.GetAppliedVersions(callingContextName);
        }

        public static IDictionary<string, List<string>> GetInstalledVersions()
        {
            return DotEntityDb.GetAllAppliedVersions();
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
                var builder = new SqlConnectionStringBuilder
                {
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