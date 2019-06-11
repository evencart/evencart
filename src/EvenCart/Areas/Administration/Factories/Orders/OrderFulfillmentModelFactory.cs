using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public class OrderFulfillmentModelFactory : IOrderFulfillmentModelFactory
    {
        private readonly IWarehouseModelFactory _warehouseModelFactory;
        private readonly IOrderModelFactory _orderModelFactory;
        public OrderFulfillmentModelFactory(IWarehouseModelFactory warehouseModelFactory, IOrderModelFactory orderModelFactory)
        {
            _warehouseModelFactory = warehouseModelFactory;
            _orderModelFactory = orderModelFactory;
        }


        public IList<OrderFulfillmentListModel> Create(IList<OrderFulfillment> entity)
        {
            var groupedByWarehouse = entity.GroupBy(x => x.Warehouse);
            var models = new List<OrderFulfillmentListModel>();
            foreach (var groupItem in groupedByWarehouse)
            {
                var warehouse = groupItem.Key;
                var fulfillments = groupItem.ToList();
                var orderItems = fulfillments.Select(x => x.OrderItem).ToList();
                var model = new OrderFulfillmentListModel()
                {
                    Warehouse = _warehouseModelFactory.Create(warehouse),
                    OrderItems = orderItems.Select(x =>
                    {
                        var m = _orderModelFactory.Create(x);
                        //update model quantity to match the fulfillment quantity
                        m.Quantity = fulfillments.First(y => y.WarehouseId == warehouse.Id && y.OrderItemId == x.Id)
                            .Quantity;
                        return m;
                    }).ToList()
                };
                models.Add(model);
            }

            return models;
        }

        public IList<OrderFulfillmentEditorModel> Create(IList<WarehouseInventory> inventories, IList<OrderFulfillment> entity)
        {
            var groupedByWarehouse = inventories.GroupBy(x => x.Warehouse);
            var models = new List<OrderFulfillmentEditorModel>();
            var orderItems = entity.Select(x => x.OrderItem).Distinct().ToList();
            foreach (var groupItem in groupedByWarehouse)
            {
                var warehouse = groupItem.Key;
                var wInventories = groupItem.ToList();
                var model = new OrderFulfillmentEditorModel()
                {
                    Warehouse = _warehouseModelFactory.Create(warehouse),
                    OrderItems = orderItems.Select(x =>
                    {
                        var availableQuantity = x.ProductVariantId > 0
                            ? wInventories.FirstOrDefault(y => y.ProductVariantId == x.ProductVariantId)
                                  ?.AvailableQuantity ?? 0
                            : wInventories.FirstOrDefault(y => y.ProductId == x.ProductVariantId)?.AvailableQuantity ??
                              0;

                        var orderItemModel = new OrderFulfillmentEditorModel.OrderItemFulfillmentModel()
                        {
                            OrderItem = _orderModelFactory.Create(x),
                            AvailableQuantity = availableQuantity,
                            CurrentQuantity = entity.FirstOrDefault(y => y.WarehouseId == warehouse.Id && y.OrderItemId == x.Id)?.Quantity ?? 0
                        };
                        if (orderItemModel.CurrentQuantity > 0 && orderItemModel.AvailableQuantity > 0)
                        {
                            orderItemModel.AvailableQuantity += orderItemModel.CurrentQuantity;
                        }
                        return orderItemModel;
                    }).Where(x => true || x.AvailableQuantity > 0).ToList() //filter out warehouses with no availability at all
                };
                models.Add(model);
            }

            return models.OrderBy(x => x.Warehouse.DisplayOrder).ToList();
        }
    }
}