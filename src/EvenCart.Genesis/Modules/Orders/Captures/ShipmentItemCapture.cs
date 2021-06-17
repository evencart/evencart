using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Orders;
using EvenCart.Services.Shipping;
using Genesis.Services.Events;

namespace EvenCart.Services.Captures
{
    public class ShipmentItemCapture : IGenesisEntityInserted<ShipmentItem>
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IPurchaseAccountant _purchaseAccountant;
        private readonly IShipmentService _shipmentService;

        public ShipmentItemCapture(IOrderItemService orderItemService, IPurchaseAccountant purchaseAccountant, IShipmentService shipmentService)
        {
            _orderItemService = orderItemService;
            _purchaseAccountant = purchaseAccountant;
            _shipmentService = shipmentService;
        }

        public void OnInserted(ShipmentItem entity)
        {
            //update shipment status
            entity.Shipment.ShipmentStatus = ShipmentStatus.Packaged;
            _shipmentService.Update(entity.Shipment);

            //update order status
            var orderItem = _orderItemService.Get(entity.OrderItemId);
            _purchaseAccountant.EvaluateOrderStatus(orderItem?.Order);
        }
    }
}