using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RoastedMarketplace.Core.Infrastructure;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Services.Installation;

namespace RoastedMarketplace.Services.Tests
{
    public abstract class BaseTest
    {
        protected bool IsAppVeyor;
        protected string MySqlConnectionString;
        protected string MsSqlConnectionString;
        protected string SqliteConnectionString;
        protected const string ContextKey = "RoastedMarketplace.Services.Tests.BaseTest";
        private readonly string _sqliteFile;

        protected BaseTest()
        {
            {
#if NETSTANDARD15
                _sqliteFile = ApplicationEnvironment.ApplicationBasePath + @"\TestDb\sqlite.db";
#else
                _sqliteFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\TestDb\sqlite.db");

#endif
                IsAppVeyor = Environment.GetEnvironmentVariable("appveyor") == "true";
                MySqlConnectionString = this.IsAppVeyor
                    ? @"Server=127.0.0.1;Uid=root;Pwd=Password12!;Database=mytest;"
                    : @"Server=127.0.0.1;Uid=root;Pwd=admin;Database=mytest;";

                MsSqlConnectionString = IsAppVeyor
                    ? @"Server=(local)\SQL2016;Database=master;User ID=sa;Password=Password12!"
                    : @"Data Source=.\sqlexpress;Initial Catalog=unittest_db;Integrated Security=False;Persist Security Info=False;User ID=iis_user;Password=iis_user";


                SqliteConnectionString = $"Data Source={_sqliteFile};";
            }

            var serviceCollection = new ServiceCollection();
            ApplicationEngine.ConfigureServices(serviceCollection, null);
        }

        [OneTimeSetUp]
        public void Setup()
        {
           
        }

        [OneTimeTearDown]
        public void Setdown()
        {
            DatabaseManager.CleanupDatabase(ContextKey);
        }

        public T Resolve<T>()
        {
            return DependencyResolver.Resolve<T>();
        }
    }
}
