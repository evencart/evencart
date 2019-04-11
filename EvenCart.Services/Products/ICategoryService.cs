using System.Collections.Generic;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Products
{
    public interface ICategoryService : IFoundationEntityService<Category>
    {
        IList<Category> GetFullCategoryTree();
    }
}