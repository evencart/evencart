using EvenCart.Services.Tests.Gdpr;
using NUnit.Framework;

namespace EvenCart.Services.Tests.MySql
{
    [TestFixture]
    public class MySqlGdprServiceTests : GdprServiceTests
    {
        public MySqlGdprServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}