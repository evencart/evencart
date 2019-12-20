using EvenCart.Services.Tests.Promotions;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlDiscountServiceTests : DiscountServiceTests
    {
        public MySqlDiscountServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}