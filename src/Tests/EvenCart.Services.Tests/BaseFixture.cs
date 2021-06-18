using System;
using EvenCart.Genesis;
using Genesis;
using Genesis.Caching;
using Genesis.Database;
using Genesis.Infrastructure;
using Genesis.Infrastructure.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EvenCart.Services.Tests
{

    public abstract class BaseFixture
    {
        static BaseFixture()
        {
            GenesisApp.Initialize(new StaticConfig(), new EvenCartDependencyContainer(), new DbVersionProvider());
        }

        protected BaseFixture()
        {


            var serviceCollection = new ServiceCollection();
            //mock the hosting env
            var hostingEnvironment = new Mock<IHostingEnvironment>();

            hostingEnvironment.Setup(x => x.ApplicationName)
                .Returns("Hosting:UnitTestEnvironment");

            hostingEnvironment.Setup(x => x.EnvironmentName)
                .Returns(GenesisApp.Current.ApplicationConfig.TestEnvironmentName);

            hostingEnvironment.Setup(x => x.ContentRootPath)
                .Returns(AppDomain.CurrentDomain.BaseDirectory);

            hostingEnvironment.Setup(x => x.ContentRootFileProvider)
                .Returns(new LocalFileProvider(hostingEnvironment.Object));

            //mock httpcontext
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            
            httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(httpContext);

            var configuration = new TestConfiguration();
            serviceCollection.AddSingleton<IHostingEnvironment>(provider => hostingEnvironment.Object);
            serviceCollection.AddSingleton<IHttpContextAccessor>(provider => httpContextAccessor.Object);
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IDatabaseSettings>(new TestDbInit.TestDatabaseSettings());


            var appEngine = GenesisApp.Current.ConfigureEngine<ApplicationEngine>(serviceCollection, hostingEnvironment.Object, configuration);
            httpContext.RequestServices = appEngine.ConfigureServices(serviceCollection, hostingEnvironment.Object, configuration);

            //set the cache providers
            CacheProviders.PrimaryProvider =
                D.Resolve<ICacheProvider>(typeof(MemoryCacheProvider).FullName);
        }
    }
}