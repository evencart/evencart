using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Warehouses
{
    public interface IWarehouseModelFactory : IModelFactory<Warehouse, WarehouseModel>
    {
        
    }
}