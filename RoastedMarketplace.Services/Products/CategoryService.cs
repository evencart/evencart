using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Core.Services;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public class CategoryService : FoundationEntityService<Category>, ICategoryService
    {
        public IList<Category> GetFullCategoryTree()
        {
            var allCategories = Repository.Select().ToList();
            
            foreach(var category in allCategories)
            {
                MakeTree(category, allCategories);
            }
            return allCategories.ToList();
        }

        private void MakeTree(Category parentCategory, IList<Category> categories)
        {
            parentCategory.ChildCategories = categories.Where(x => x.ParentCategoryId == parentCategory.Id).ToList();
            foreach (var c in parentCategory.ChildCategories)
                c.ParentCategory = parentCategory;
        }
    }
}