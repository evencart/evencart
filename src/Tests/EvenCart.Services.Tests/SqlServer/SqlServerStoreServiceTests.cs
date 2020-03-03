using EvenCart.Services.Tests.Stores;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerStoreServiceTests : StoreServiceTests
    {
        public SqlServerStoreServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}