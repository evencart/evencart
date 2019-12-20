using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerUserAuthenticationTests : UserAuthenticationTests
    {
        public SqlServerUserAuthenticationTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}