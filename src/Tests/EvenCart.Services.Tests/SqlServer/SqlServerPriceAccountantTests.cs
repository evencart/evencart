using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerPriceAccountantTests : PriceAccountantTests
    {
        public SqlServerPriceAccountantTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}