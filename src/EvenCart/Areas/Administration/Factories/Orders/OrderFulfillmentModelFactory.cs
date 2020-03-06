#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;

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
            foreach (var groupItem in groupedByWarehouse)
            {
                var warehouse = groupItem.Key;
                var wInventories = groupItem.ToList();
                var model = new OrderFulfillmentEditorModel()
                {
                    Warehouse = _warehouseModelFactory.Create(warehouse),
                    OrderItems = entity.Select(x =>
                    {
                        var orderItem = x.OrderItem;
                        var orderItemModel = new OrderFulfillmentEditorModel.OrderItemFulfillmentModel()
                        {
                            OrderItem = _orderModelFactory.Create(orderItem),
                            AvailableQuantity = 0,
                            CurrentQuantity = x.Quantity,
                            Locked = x.Locked
                        };

                        if (x.Locked)
                            return orderItemModel;

                        var availableQuantity = orderItem.ProductVariantId > 0
                            ? wInventories.FirstOrDefault(y => y.ProductVariantId == orderItem.ProductVariantId)
                                  ?.AvailableQuantity ?? 0
                            : wInventories.FirstOrDefault(y => y.ProductId == orderItem.ProductId)?.AvailableQuantity ??
                              0;
                        orderItemModel.AvailableQuantity = availableQuantity;
                        orderItemModel.CurrentQuantity = x.Quantity;
                        if (orderItemModel.CurrentQuantity > 0 && orderItemModel.AvailableQuantity > 0)
                        {
                            orderItemModel.AvailableQuantity += orderItemModel.CurrentQuantity;
                        }
                        return orderItemModel;
                    }).ToList()                    
                };
                models.Add(model);
            }

            return models.OrderBy(x => x.Warehouse.DisplayOrder).ToList();
        }
    }
}