using System.Linq;
using EvenCart.Areas.Administration.Models.Catalog;
using EvenCart.Areas.Administration.Models.Store;
using EvenCart.Data.Entity.Shop;


namespace EvenCart.Areas.Administration.Factories.Catalogs
{
    public class CatalogModelFactory : ICatalogModelFactory
    {
        public CatalogModel Create(Catalog entity)
        {
            return new CatalogModel()
            {
                Name = entity.Name,
                Id = entity.Id
            };
        }
    }
}