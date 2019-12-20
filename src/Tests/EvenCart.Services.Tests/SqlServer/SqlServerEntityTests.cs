using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerEntityTests : EntityTests
    {
        public SqlServerEntityTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}