using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlCategoryServiceTests : CategoryServiceTests
    {
        public MySqlCategoryServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}