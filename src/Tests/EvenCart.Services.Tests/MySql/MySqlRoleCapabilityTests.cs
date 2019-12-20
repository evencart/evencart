using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlRoleCapabilityTests : RoleCapabilityTests
    {
        public MySqlRoleCapabilityTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}