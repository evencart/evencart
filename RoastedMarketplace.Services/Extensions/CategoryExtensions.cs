using System;
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
    }
}