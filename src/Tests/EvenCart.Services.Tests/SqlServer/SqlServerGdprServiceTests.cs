using EvenCart.Services.Tests.Gdpr;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerGdprServiceTests : GdprServiceTests
    {
        public SqlServerGdprServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}