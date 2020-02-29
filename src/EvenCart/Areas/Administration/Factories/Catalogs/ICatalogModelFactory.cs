using EvenCart.Areas.Administration.Models.Catalog;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Catalogs
{
    public interface ICatalogModelFactory : IModelFactory<Catalog, CatalogModel>
    {
        
    }
}