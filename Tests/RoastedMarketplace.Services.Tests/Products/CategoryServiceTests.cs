using NUnit.Framework;
using RoastedMarketplace.Services.Products;

namespace RoastedMarketplace.Services.Tests.Products
{
    public abstract class CategoryServiceTests : BaseTest
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryAccountant _categoryAccountant;
        protected CategoryServiceTests()
        {
            _categoryService = Resolve<ICategoryService>();
            _categoryAccountant = Resolve<ICategoryAccountant>();
        }

        [Test]
        public void Category_Tests_Succeed()
        {
            var allCategories = _categoryService.GetFullCategoryTree();
            var clothingCategory = _categoryAccountant.CreateCategoryTree("Clothing", allCategories);
            var shirtCategory = _categoryAccountant.CreateCategoryTree("Clothing > Shirts", allCategories);
            var tShirtCategory = _categoryAccountant.CreateCategoryTree("Clothing > Shirts > T-Shirts", allCategories);
            var tShirtCategory2 = _categoryAccountant.CreateCategoryTree("Clothing>Shirts>T-Shirts", allCategories);

            Assert.AreEqual(clothingCategory.Id, shirtCategory.ParentCategoryId);
            Assert.AreEqual(shirtCategory.Id, tShirtCategory.ParentCategoryId);
            Assert.AreEqual(tShirtCategory.Id, tShirtCategory2.Id);
        }
    }

    [TestFixture]
    public class SqlServerCategoryPersistanceTests : CategoryServiceTests
    {
        public SqlServerCategoryPersistanceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}