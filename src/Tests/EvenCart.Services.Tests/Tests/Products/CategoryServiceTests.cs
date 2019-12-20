using EvenCart.Services.Products;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Products
{
    public abstract class CategoryServiceTests : BaseTest
    {
        private ICategoryService _categoryService;
        private ICategoryAccountant _categoryAccountant;
        
        [SetUp]
        public void SetUp()
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
            var tShirtCategory3 = _categoryAccountant.CreateCategoryTree("clothing>shirts > t-SHIRTS", allCategories);
            Assert.AreEqual(clothingCategory.Id, shirtCategory.ParentCategoryId);
            Assert.AreEqual(shirtCategory.Id, tShirtCategory.ParentCategoryId);
            Assert.AreEqual(tShirtCategory.Id, tShirtCategory2.Id);
            Assert.AreEqual(tShirtCategory.Id, tShirtCategory3.Id);
        }
    }
}