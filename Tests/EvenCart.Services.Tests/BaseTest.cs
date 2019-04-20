using System;
using System.Collections.Generic;
using System.IO;
using EvenCart.Core.Infrastructure;
using EvenCart.Core.Infrastructure.Providers;
using EvenCart.Data.Database;
using EvenCart.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;

namespace EvenCart.Services.Tests
{
    public class BaseTest
    {
        protected bool IsAppVeyor;
        protected string MySqlConnectionString;
        protected string MsSqlConnectionString;
        protected string SqliteConnectionString;
        protected const string ContextKey = "EvenCart";
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
                    : @"Data Source=.;Initial Catalog=unittest_db;Integrated Security=False;Persist Security Info=False;User ID=iis_user;Password=iis_user";


                SqliteConnectionString = $"Data Source={_sqliteFile};";
            }

            var serviceCollection = new ServiceCollection();
            //mock the hosting env
            var hostingEnvironment = new Mock<IHostingEnvironment>();
            
            hostingEnvironment.Setup(x => x.ApplicationName)
                .Returns("Hosting:UnitTestEnvironment");

            hostingEnvironment.Setup(x => x.ContentRootPath)
                .Returns(AppDomain.CurrentDomain.BaseDirectory);

            hostingEnvironment.Setup(x => x.ContentRootFileProvider)
                .Returns(new LocalFileProvider(hostingEnvironment.Object));

            serviceCollection.AddSingleton<IHostingEnvironment>(provider => hostingEnvironment.Object);
            serviceCollection.AddSingleton<IConfiguration>(new TestConfiguration());
            ApplicationEngine.ConfigureServices(serviceCollection, hostingEnvironment.Object);
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
