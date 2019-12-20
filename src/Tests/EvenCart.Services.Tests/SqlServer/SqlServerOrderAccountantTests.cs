using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerOrderAccountantTests : OrderAccountantTests
    {
        public SqlServerOrderAccountantTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}