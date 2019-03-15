using RoastedMarketplace.Data.Entity.Shop;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Products;

namespace RoastedMarketplace.Factories.Products
{
    public interface IProductModelFactory : IModelFactory<Product, ProductModel>
    {
        
    }
}