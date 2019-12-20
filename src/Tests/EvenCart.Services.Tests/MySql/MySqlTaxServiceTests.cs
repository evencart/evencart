using EvenCart.Services.Tests.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlTaxServiceTests : TaxServiceTests
    {
        public MySqlTaxServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}