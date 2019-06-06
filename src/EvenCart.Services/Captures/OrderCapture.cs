using System.Linq;
using EvenCart.Core.Services;
using EvenCart.Core.Services.Events;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;

namespace EvenCart.Services.Captures
{
    public class OrderCapture : IFoundationEntityInserted<Order>, IFoundationEntityUpdated<Order>
    {
        private readonly IOrderAccountant _orderAccountant;
        private readonly IOrderFulfillmentService _orderFulfillmentService;
        private readonly IWarehouseInventoryService _warehouseInventoryService;
        public OrderCapture(IOrderAccountant orderAccountant, IOrderFulfillmentService orderFulfillmentService, IWarehouseInventoryService warehouseInventoryService)
        {
            _orderAccountant = orderAccountant;
            _orderFulfillmentService = orderFulfillmentService;
            _warehouseInventoryService = warehouseInventoryService;
        }

        public void OnInserted(Order entity)
        {
            //do we have some auto fulfillments
            var fulfillments = _orderAccountant.GetAutoOrderFulfillments(entity);
            if (fulfillments != null)
            {
                Transaction.Initiate(transaction =>
                {
                    foreach (var fulfillment in fulfillments)
                    {
                        fulfillment.WarehouseInventory.ReservedQuantity += fulfillment.Quantity;
                        //update inventory
                        _warehouseInventoryService.Update(fulfillment.WarehouseInventory, transaction);
                        _orderFulfillmentService.Insert(fulfillment, transaction);
                    }
                });
            }
        }

        public void OnUpdated(Order entity)
        {
            
        }
    }
}