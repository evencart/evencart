using System.Linq;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Extensions
{
    public static class CategoryExtensions
    {
        public static bool IsAnyChild(this Category category, int childCategoryId)
        {
            if (category?.Children == null)
                return false;

            return category.Children.Any(x => x.Id == childCategoryId || IsAnyChild(x, childCategoryId));
        }

        public static string GetCategoryPath(this Category category)
        {
            if (category == null)
                return null;
            var c = category.Parent;
            var categoryPath = "";
            while (c != null)
            {
                categoryPath = $"{c.SeoMeta.Slug}/" + categoryPath;
                c = c.Parent;
            }
            categoryPath = categoryPath.TrimEnd('/');
            return categoryPath;
        }
    }
}