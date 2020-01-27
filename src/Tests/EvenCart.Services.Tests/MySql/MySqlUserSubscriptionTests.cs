using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlUserSubscriptionTests : UserSubscriptionTests
    {
        public MySqlUserSubscriptionTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}