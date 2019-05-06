using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Purchases;
using EvenCart.Services.Shipping;

namespace EvenCart.Services.Captures
{
    public class ShipmentItemCapture : IFoundationEntityInserted<ShipmentItem>
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