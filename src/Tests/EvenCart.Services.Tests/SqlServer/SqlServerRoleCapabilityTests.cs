using EvenCart.Services.Tests.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.SqlServer
{
    [TestFixture]
    public class SqlServerRoleCapabilityTests : RoleCapabilityTests
    {
        public SqlServerRoleCapabilityTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}