using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerUserSubscriptionTests : UserSubscriptionTests
    {
        public SqlServerUserSubscriptionTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}