using System;
using System.Collections.Generic;
using System.Linq;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Extensions
{
    public static class CategoryExtensions
    {
        public static bool IsAnyChild(this Category category, int childCategoryId)
        {
            if (category?.ChildCategories == null)
                return false;

            return category.ChildCategories.Any(x => x.Id == childCategoryId || IsAnyChild(x, childCategoryId));
        }

        public static string GetCategoryPath(this Category category, IList<Category> categoryTree)
        {
            if (category == null)
                return null;
            var c = category.ParentCategory;
            var categoryPath = "";
            while (c != null)
            {
                categoryPath = $"{c.SeoMeta.Slug}/" + categoryPath;
                c = c.ParentCategory;
            }
            categoryPath = categoryPath.TrimEnd('/');
            return categoryPath;
        }
    }
}