using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlProductServiceTests : ProductServiceTests
    {
        public MySqlProductServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}