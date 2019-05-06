using System.Collections.Generic;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface ICategoryAccountant
    {
        string GetFullBreadcrumb(Category category, string separator =  " > ");

        Category CreateCategoryTree(string categoryTree, IList<Category> allCategories, string separator = ">");
    }
}