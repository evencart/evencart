using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Products;

namespace EvenCart.Factories.Products
{
    public interface IProductModelFactory : IModelFactory<Product, ProductModel>
    {
        
    }
}