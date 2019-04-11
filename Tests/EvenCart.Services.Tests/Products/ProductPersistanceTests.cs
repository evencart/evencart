using EvenCart.Services.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Products
{
    public abstract class ProductPersistanceTests : BaseTest
    {
        private readonly IProductService _productService;
        protected ProductPersistanceTests()
        {
            _productService = Resolve<IProductService>();
        }

        [Test]
        public void products_are_saved_in_database()
        {
            var products = EvenCart.Tests.Data.Products.GetList();
            foreach(var p in products)
                _productService.Insert(p);

            Assert.AreEqual(1, products[0].Id);
            Assert.AreEqual(2, products[1].Id);
        }

    }

    [TestFixture]
    public class SqlServerPersistanceTests : ProductPersistanceTests
    {
        public SqlServerPersistanceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}