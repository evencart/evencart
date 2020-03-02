using EvenCart.Services.Tests.Stores;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlStoreServiceTests : StoreServiceTests
    {
        public MySqlStoreServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}