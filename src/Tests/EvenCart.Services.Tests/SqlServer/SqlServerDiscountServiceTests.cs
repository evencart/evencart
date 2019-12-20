using EvenCart.Services.Tests.Promotions;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerDiscountServiceTests : DiscountServiceTests
    {
        public SqlServerDiscountServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}