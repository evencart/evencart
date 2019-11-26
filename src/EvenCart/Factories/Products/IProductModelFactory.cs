using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using DownloadModel = EvenCart.Models.Products.DownloadModel;
using ProductModel = EvenCart.Models.Products.ProductModel;

namespace EvenCart.Factories.Products
{
    public interface IProductModelFactory : IModelFactory<Product, ProductModel>
    {
        DownloadModel Create(Download download);        
    }
}