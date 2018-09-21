using System.Collections.Generic;
using RoastedMarketplace.Data.Entity.Shop;

namespace RoastedMarketplace.Services.Products
{
    public interface ICategoryAccountant
    {
        string GetFullBreadcrumb(Category category, string separator =  " > ");

        Category CreateCategoryTree(string categoryTree, IList<Category> allCategories, string separator = ">");
    }
}