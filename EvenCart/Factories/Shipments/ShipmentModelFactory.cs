using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Shipments;

namespace EvenCart.Factories.Shipments
{
    public class ShipmentModelFactory : IShipmentModelFactory
    {
        private readonly IModelMapper _modelMapper;
        public ShipmentModelFactory(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public ShipmentModel Create(Shipment entity)
        {
            var model = _modelMapper.Map<ShipmentModel>(entity);
            model.ShipmentItems = entity.ShipmentItems.Select(Create).ToList();
            return model;
        }

        public ShipmentItemModel Create(ShipmentItem entity)
        {
            var shipmentItemModel = new ShipmentItemModel()
            {
                Quantity = entity.Quantity,
                ProductName =  entity.OrderItem.Product.Name,
                OrderedQuantity = entity.OrderItem.Quantity,
                ShippedQuantity = entity.Quantity,
                OrderItemId = entity.OrderItemId
            };
            return shipmentItemModel;
        }
    }
}