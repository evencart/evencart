using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlCartServiceTests : CartServiceTests
    {
        public MySqlCartServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}