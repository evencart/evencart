using EvenCart.Areas.Administration.Factories.Addresses;
using EvenCart.Areas.Administration.Models.Warehouse;
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Warehouses
{
    public class WarehouseModelFactory : IWarehouseModelFactory
    {
        private readonly IAddressModelFactory _addressModelFactory;
        public WarehouseModelFactory(IAddressModelFactory addressModelFactory)
        {
            _addressModelFactory = addressModelFactory;
        }

        public WarehouseModel Create(Warehouse entity)
        {
            var model = new WarehouseModel()
            {
                Id = entity.Id,
                DisplayOrder = entity.DisplayOrder
            };
            if (entity.Address != null)
                model.Address = _addressModelFactory.Create(entity.Address);
            return model;
        }

        public WarehouseMiniModel  CreateMini(Warehouse entity)
        {
            var model = new WarehouseMiniModel()
            {
                Id = entity.Id,
                Name = entity.Address?.Name,
                DisplayOrder = entity.DisplayOrder
            };
            return model;
        }
    }
}