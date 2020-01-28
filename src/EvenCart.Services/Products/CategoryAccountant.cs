using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Core.Extensions;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public class CategoryAccountant : ICategoryAccountant
    {
        private readonly ICategoryService _categoryService;
        public CategoryAccountant(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public string GetFullBreadcrumb(Category category, string separator = " > ")
        {
            return category.GetNameBreadCrumb(separator);
        }

        public Category CreateCategoryTree(string categoryTree, IList<Category> allCategories, string separator = ">")
        {
            if (string.IsNullOrEmpty(categoryTree))
                return null;
            //we are getting categories in the format a > b > c, so let's split them up and create such a tree
            var categoryParts = categoryTree.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            if (!categoryParts.Any())
                return null;

            //search for all matching categories
            var previousId = 0;
            Category resultantCategory = null;
            foreach (var categoryName in categoryParts) {
                var expectedParentId = previousId;
                var category = allCategories.FirstOrDefault(x => x.ParentId == expectedParentId && x.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase));

                // we didn't find this category, so it's a new category that needs to be created
                if (category == null)
                {
                    //insert this category
                    category = new Category()
                    {
                        Name = categoryName,
                        ParentId = expectedParentId
                    };
                    _categoryService.Insert(category);
                    //add to all categories
                    allCategories.Add(category);
                }
                //now we are good, we can use this as next's parent
                previousId = category.Id;
                resultantCategory = category;
            }
            return resultantCategory;
        }
    }
}