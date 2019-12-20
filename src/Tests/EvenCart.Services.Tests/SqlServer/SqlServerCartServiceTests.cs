using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerCartServiceTests : CartServiceTests
    {
        public SqlServerCartServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}