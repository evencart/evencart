using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerProductServiceTests : ProductServiceTests
    {
        public SqlServerProductServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}