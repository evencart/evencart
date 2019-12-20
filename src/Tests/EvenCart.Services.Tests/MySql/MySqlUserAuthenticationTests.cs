using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlUserAuthenticationTests : UserAuthenticationTests
    {
        public MySqlUserAuthenticationTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}