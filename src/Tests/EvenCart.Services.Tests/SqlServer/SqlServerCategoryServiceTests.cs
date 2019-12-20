using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerCategoryServiceTests : CategoryServiceTests
    {
        public SqlServerCategoryServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}