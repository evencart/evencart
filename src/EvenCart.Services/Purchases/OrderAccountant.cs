using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Services.Products;

namespace EvenCart.Services.Purchases
{
    public class OrderAccountant : IOrderAccountant
    {
        private readonly IWarehouseInventoryService _warehouseInventoryService;
        public OrderAccountant(IWarehouseInventoryService warehouseInventoryService)
        {
            _warehouseInventoryService = warehouseInventoryService;
        }
        /// <summary>
        /// Gets order fulfillments based on the following rules.
        /// - If all the products in an order are present in multiple warehouses, select a warehouse that has lowest order and can fulfill all the products
        /// - If all the products in an order are present in only one of the warehouses, select that warehouse irrespective of order
        /// - If some products are present in one warehouse and others are present in other warehouse, apply warehouse selection based on their order for each product.
        /// </summary>
        private IList<OrderFulfillment> GetAutoOrderFulfillments(Order order, OrderFulfillmentType orderFulfillmentType)
        {
            //the product ids for this order
            var productIds = order.OrderItems.Select(x => x.ProductId).Distinct().ToList();
            var warehouseInventories = _warehouseInventoryService.GetByProducts(productIds).ToList();
            var availableWarehouses = warehouseInventories.Select(x => x.Warehouse).Distinct().OrderBy(x => x.DisplayOrder).ToList();
            var fulfillments = new List<OrderFulfillment>();
            foreach (var warehouse in availableWarehouses)
            {
                var wInventories = warehouseInventories.Where(x => x.WarehouseId == warehouse.Id).ToList();
                var warehouseCheckSuccess = true;
                foreach (var orderItem in order.OrderItems)
                {
                    if (fulfillments.Any(x => x.OrderItemId == orderItem.Id && x.Quantity == orderItem.Quantity))
                        continue; //do we already have fulfilled this item, yes and so skip the item now

                    WarehouseInventory warehouseInventory;
                    if (orderItem.ProductVariantId > 0)
                    {
                        warehouseInventory = wInventories.FirstOrDefault(x =>
                            x.ProductVariantId == orderItem.ProductVariantId &&
                            x.AvailableQuantity >= orderItem.Quantity);

                        //do we have any inventory for this variant in this warehouse, if not, we don't need to proceed
                        if (warehouseInventory == null)
                        {
                            if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                            {
                                warehouseCheckSuccess = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        warehouseInventory = wInventories.FirstOrDefault(x =>
                            x.ProductId == orderItem.ProductId &&
                            x.AvailableQuantity >= orderItem.Quantity);
                        //do we have any inventory for this product in this warehouse, if not, we don't need to proceed
                        if (warehouseInventory == null)
                        {
                            if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                            {
                                warehouseCheckSuccess = false;
                                break;
                            }
                        }
                    }
                    //add only if fulfillment can be done
                    if (warehouseInventory?.AvailableQuantity >= orderItem.Quantity)
                    {
                        fulfillments.Add(new OrderFulfillment()
                        {
                            Warehouse = warehouse,
                            Order = order,
                            OrderItem = orderItem,
                            OrderId = order.Id,
                            OrderItemId = orderItem.Id,
                            Quantity = orderItem.Quantity,
                            WarehouseId = warehouse.Id,
                            Verified = false,
                            WarehouseInventory = warehouseInventory
                        });
                    }
                    else
                    {
                        warehouseCheckSuccess = false;
                        break;
                    }
                }

                if (!warehouseCheckSuccess)
                {
                    if (orderFulfillmentType == OrderFulfillmentType.WholeFromSingleWarehouse)
                        fulfillments.Clear(); //avoid the fulfillment by this warehouse
                }
                else
                {
                    return fulfillments; //we got the warehouse, so return
                }
            }

            return fulfillments;
        }

        public IList<OrderFulfillment> GetAutoOrderFulfillments(Order order)
        {
            foreach (var fulfillmentType in new List<OrderFulfillmentType>()
            {
                OrderFulfillmentType.WholeFromSingleWarehouse,
                OrderFulfillmentType.SplitToMultipleWarehouseByItem,
                OrderFulfillmentType.SplitToMultipleWarehouseByItemQuantity
            })
            {
                var fulfillment = GetAutoOrderFulfillments(order, fulfillmentType);
                if (fulfillment.Any())
                    return fulfillment;
            }

            return null;
        }
    }
}