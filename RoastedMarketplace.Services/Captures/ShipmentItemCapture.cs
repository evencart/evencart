using RoastedMarketplace.Core.Services.Events;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Shipping;

namespace RoastedMarketplace.Services.Captures
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