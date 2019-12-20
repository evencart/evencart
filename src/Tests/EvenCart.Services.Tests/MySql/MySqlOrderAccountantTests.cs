using EvenCart.Services.Tests.Purchases;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlOrderAccountantTests : OrderAccountantTests
    {
        public MySqlOrderAccountantTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}