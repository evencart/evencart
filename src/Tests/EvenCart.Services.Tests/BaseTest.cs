using System;
using System.Collections.Generic;
using System.IO;
using Genesis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace EvenCart.Services.Tests
{
    public class BaseTest
    {
        protected static bool IsAppVeyor;
        public static string MySqlConnectionString;
        public static string MsSqlConnectionString;
        public static string SqliteConnectionString;
        public const string ContextKey = "EvenCart";
        private static readonly string _sqliteFile;

        static BaseTest()
        {
            {
#if NETSTANDARD15
                _sqliteFile = ApplicationEnvironment.ApplicationBasePath + @"\TestDb\sqlite.db";
#else
                _sqliteFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\TestDb\sqlite.db");

#endif
                IsAppVeyor = Environment.GetEnvironmentVariable("appveyor") == "true";
                var isDeploymentServer = Environment.GetEnvironmentVariable("env.dotentity_deployment") == "true";
                MySqlConnectionString = IsAppVeyor
                    ? @"Server=127.0.0.1;Uid=root;Pwd=Password12!;Database=mytest;"
                    : @"Server=localhost;Uid=root;Pwd=admin;Database=unittest;";

                MsSqlConnectionString = IsAppVeyor
                    ? @"Server=(local)\SQL2016;Database=master;User ID=sa;Password=Password12!"
                    : isDeploymentServer
                        ? @"Data Source=.\SqlExpress;Initial Catalog=unittest_db;Integrated Security=True;"
                        : @"Data Source=.;Initial Catalog=unittest_db;Integrated Security=True;Persist Security Info=False;User ID=iis_user;Password=iis_user";


                SqliteConnectionString = $"Data Source={_sqliteFile};";
            }
        }

        public T Resolve<T>()
        {
            return D.Resolve<T>();
        }

        public T Resolve<T>(string serviceKey)
        {
            return D.Resolve<T>(serviceKey);
        }
    }

    public class TestConfiguration : IConfiguration
    {
        public IConfigurationSection GetSection(string key)
        {
            return null;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return null;
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public string this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
