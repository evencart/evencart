using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlPriceAccountantTests : PriceAccountantTests
    {
        public MySqlPriceAccountantTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}