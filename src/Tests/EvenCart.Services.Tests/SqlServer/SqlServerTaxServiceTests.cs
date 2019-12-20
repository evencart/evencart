using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerTaxServiceTests : TaxServiceTests
    {
        public SqlServerTaxServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}