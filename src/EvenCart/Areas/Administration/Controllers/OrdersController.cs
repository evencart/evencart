using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Orders;
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Core.Services;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Shop;
using EvenCart.Data.Extensions;
using EvenCart.Services.Formatter;
using EvenCart.Services.Purchases;
using EvenCart.Services.Serializers;
using EvenCart.Services.Shipping;
using EvenCart.Events;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    public class OrdersController : FoundationAdminController
    {
        private readonly IOrderService _orderService;
        private readonly IModelMapper _modelMapper;
        private readonly IDataSerializer _dataSerializer;
        private readonly IFormatterService _formatterService;
        private readonly IShipmentService _shipmentService;
        private readonly IShipmentItemService _shipmentItemService;
        private readonly IShipmentStatusHistoryService _shipmentStatusHistoryService;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IWarehouseService _warehouseService;
        private readonly IWarehouseInventoryService _warehouseInventoryService;
        private readonly IOrderFulfillmentService _orderFulfillmentService;
        private readonly IOrderFulfillmentModelFactory _orderFulfillmentModelFactory;
        private readonly IOrderItemService _orderItemService;
        private readonly IWarehouseModelFactory _warehouseModelFactory;
        private readonly IShipmentModelFactory _shipmentModelFactory;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IReturnRequestModelFactory _returnRequestModelFactory;
        private readonly IOrderAccountant _orderAccountant;
        private readonly IPurchaseAccountant _purchaseAccountant;
        public OrdersController(IOrderService orderService, IModelMapper modelMapper, IDataSerializer dataSerializer, IFormatterService formatterService, IShipmentService shipmentService, IShipmentItemService shipmentItemService, IShipmentStatusHistoryService shipmentStatusHistoryService, IOrderModelFactory orderModelFactory, IWarehouseService warehouseService, IWarehouseInventoryService warehouseInventoryService, IOrderFulfillmentService orderFulfillmentService, IOrderFulfillmentModelFactory orderFulfillmentModelFactory, IOrderItemService orderItemService, IWarehouseModelFactory warehouseModelFactory, IShipmentModelFactory shipmentModelFactory, IReturnRequestService returnRequestService, IReturnRequestModelFactory returnRequestModelFactory, IOrderAccountant orderAccountant, IPurchaseAccountant purchaseAccountant)
        {
            _orderService = orderService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _formatterService = formatterService;
            _shipmentService = shipmentService;
            _shipmentItemService = shipmentItemService;
            _shipmentStatusHistoryService = shipmentStatusHistoryService;
            _orderModelFactory = orderModelFactory;
            _warehouseService = warehouseService;
            _warehouseInventoryService = warehouseInventoryService;
            _orderFulfillmentService = orderFulfillmentService;
            _orderFulfillmentModelFactory = orderFulfillmentModelFactory;
            _orderItemService = orderItemService;
            _warehouseModelFactory = warehouseModelFactory;
            _shipmentModelFactory = shipmentModelFactory;
            _returnRequestService = returnRequestService;
            _returnRequestModelFactory = returnRequestModelFactory;
            _orderAccountant = orderAccountant;
            _purchaseAccountant = purchaseAccountant;
        }

        #region Orders

        [DualGet("", Name = AdminRouteNames.OrdersList)]
        [CapabilityRequired(CapabilitySystemNames.ViewOrder)]
        public IActionResult OrdersList([FromQuery] OrderSearchModel searchModel)
        {
            searchModel = searchModel ?? new OrderSearchModel();

            var orders = _orderService.GetOrders(out int totalResults, searchModel.SearchPhrase, searchModel.UserId, searchModel.OrderIds, searchModel.ProductIds,
                searchModel.OrderStatus, searchModel.PaymentStatus, searchModel.VendorIds, searchModel.FromDate,
                searchModel.ToDate, searchModel.Current, searchModel.RowCount);

            var orderModels = orders.Select(_orderModelFactory.Create).ToList();

            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("orders", () => orderModels, () => _dataSerializer.Serialize(orderModels))
                .WithAvailableShipmentStatusTypes()
                .WithAvailableOrderStatusTypes()
                .WithAvailablePaymentStatusTypes()
                .WithParams(searchModel)
                .Result;
        }

        [DualGet("{orderId}", Name = AdminRouteNames.GetOrder)]
        [CapabilityRequired(CapabilitySystemNames.EditOrder)]
        public IActionResult OrderEditor(int orderId)
        {
            var order = orderId > 0 ? _orderService.Get(orderId) : new Order();
            if (order == null)
                return NotFound();
            var orderModel = _orderModelFactory.Create(order);
            return R.Success.With("order", orderModel).Result;
        }

        #endregion

        #region Order Fulfillments

        [DualGet("{orderId}/fulfillments", Name = AdminRouteNames.OrderFulfillmentsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageInventory)]
        public IActionResult OrderFulfillmentsList(int orderId)
        {
            var order = orderId > 0 ? _orderService.Get(orderId) : null;
            if (order == null)
                return NotFound();
            var fulfillments = _orderFulfillmentService.Get(x => x.OrderId == orderId).ToList();
            var models = _orderFulfillmentModelFactory.Create(fulfillments);
            var canEditFulfillments = order.Shipments == null || !order.Shipments.Any();
            return R.Success.With("orderFulfillments", models).With("canEditFulfillments", canEditFulfillments).Result;
        }

        [DualGet("{orderId}/fulfillment", Name = AdminRouteNames.GetOrderFulfillment)]
        [CapabilityRequired(CapabilitySystemNames.ManageInventory)]
        public IActionResult OrderFulfillmentEditor(int orderId)
        {
            var order = orderId > 0 ? _orderService.Get(orderId) : null;
            if (order == null)
                return NotFound();
            if (order.Shipments?.Any() ?? false)
                return R.Fail.With("error", "Unable to update order fulfillments after shipments have been added")
                    .Result;

            //get the warehouses where orderitems for this order are available
            var productIds = order.OrderItems.Select(x => x.ProductId).ToList();
            var inventories = _warehouseInventoryService.GetByProducts(productIds).ToList();
            var fulfillments = _orderFulfillmentService.Get(x => x.OrderId == orderId).ToList();
            var models = _orderFulfillmentModelFactory.Create(inventories, fulfillments);
            var totalOrderedCount = order.OrderItems.Sum(x => x.Quantity);
            return R.Success.With("orderFulfillments", models).With("orderedItemsCount", totalOrderedCount).Result;
        }

        [DualPost("{orderId}/fulfillment", Name = AdminRouteNames.SaveOrderFulfillment, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageInventory)]
        public IActionResult SaveOrderFulfillment(int orderId, IList<OrderFulfillmentModel> orderFulfillments)
        {
            if (orderFulfillments == null || !orderFulfillments.Any())
                return BadRequest();

            var order = orderId > 0 ? _orderService.Get(orderId) : null;
            if (order == null)
                return NotFound();

            if (order.Shipments?.Any() ?? false)
                return R.Fail.With("error", "Unable to update order fulfillments after shipments have been added")
                    .Result;

            var orderItemIds = orderFulfillments.Select(x => x.OrderItemId).Distinct().ToList();
            var warehouseIds = orderFulfillments.Select(x => x.WarehouseId).Distinct().ToList();
            if (order.OrderItems.Count > orderItemIds.Count)
                return R.Fail.With("error", T("All order item ids must be provided to update any fulfillment")).Result;

            var orderItems = _orderItemService.GetWithProducts(orderItemIds).ToList();
            //get the saved fulfillments
            var savedFulfillments = _orderFulfillmentService
                .Get(x => warehouseIds.Contains(x.WarehouseId) && orderItemIds.Contains(x.OrderItemId)).ToList();

            var warehouses = _warehouseService.Get(x => warehouseIds.Contains(x.Id)).ToList();
            //first validate if provided distribution can be done
            foreach (var orderItem in orderItems)
            {
                var itemModel = orderFulfillments.First(x => x.OrderItemId == orderItem.Id);
                var quantityRequested = itemModel.Quantity;
                var warehouseId = itemModel.WarehouseId;
                var warehouse = warehouses.First(x => x.Id == warehouseId);
                if (warehouse == null)
                {
                    return R.Fail.With("error", T("Unable to find one or more of provided warehouse ids")).Result;
                }

                var savedFullfilment =
                    savedFulfillments.FirstOrDefault(x =>
                        x.OrderItemId == orderItem.Id && x.WarehouseId == warehouseId);
                if (savedFullfilment != null && savedFullfilment.Quantity <= quantityRequested)
                {
                    //nothing needs to be done here, as the new fulfillment scheme for this order item is same as the saved one
                    continue;
                }

                if (savedFullfilment != null && savedFullfilment.Quantity < quantityRequested)
                {
                    //do we have an increment or decrement of quantity...if it's a decrement, no issues (already tackled above), but if it's an increment, we need to check for 
                    //additional quantities
                    quantityRequested = quantityRequested - savedFullfilment.Quantity; //let's just check for those additional quantities
                }
                //do we've a product variant?
                if (orderItem.ProductVariantId > 0)
                {
                    //is the requested quantity available in requested warehouse
                    if (!orderItem.ProductVariant.IsAvailableInStock(quantityRequested, warehouseId))
                    {
                        return R.Fail.With("error",
                            T("The product '{0}' is not sufficiently available in warehouse '{1}'",
                                arguments: new object[] { orderItem.Product.Name, warehouse.Address.Name })).Result;
                    }
                }
                else
                {
                    //is the requested quantity available in requested warehouse
                    if (!orderItem.Product.IsAvailableInStock(quantityRequested, warehouseId))
                    {
                        return R.Fail.With("error",
                            T("The product '{0}' is not sufficiently available in warehouse '{1}'",
                                arguments: new object[] { orderItem.Product.Name, warehouse.Address.Name })).Result;
                    }
                }
            }

            //if we are here, that means all the requested updates can be done
            //perform all the updates in a transaction to avoid any conflicts
            var success = Transaction.Initiate(transaction =>
            {
                foreach (var itemModel in orderFulfillments)
                {
                    var orderItem = orderItems.First(x => x.Id == itemModel.OrderItemId);
                    var quantityRequested = itemModel.Quantity;
                    var warehouseId = itemModel.WarehouseId;
                    var savedFullfilment =
                        savedFulfillments.FirstOrDefault(x =>
                            x.OrderItemId == orderItem.Id && x.WarehouseId == warehouseId);

                    var inventory = orderItem.GetWarehouseInventory(warehouseId);
                    if (savedFullfilment != null)
                    {
                        if (savedFullfilment.Quantity == quantityRequested)
                            //nothing needs to be done here, as the new fulfillment scheme for this order item is same as the saved one
                            continue;
                        if (savedFullfilment.Quantity > quantityRequested)
                        {
                            inventory.ReservedQuantity =
                                inventory.ReservedQuantity - (savedFullfilment.Quantity - quantityRequested);
                            //remove the difference from reserves
                        }
                        else
                        {
                            inventory.ReservedQuantity =
                                inventory.ReservedQuantity + (quantityRequested - savedFullfilment.Quantity);
                            //add the difference to reserves
                        }

                        savedFullfilment.Quantity = quantityRequested;
                        //update both
                        _warehouseInventoryService.Update(inventory, transaction);
                        _orderFulfillmentService.Update(savedFullfilment, transaction);
                    }
                    else
                    {
                        //we'll add this new fulfillment
                        savedFullfilment = new OrderFulfillment()
                        {
                            OrderItemId = orderItem.Id,
                            WarehouseId = warehouseId,
                            OrderId = orderId,
                            Quantity = quantityRequested,
                            Verified = true
                        };
                        _orderFulfillmentService.Insert(savedFullfilment, transaction);
                        inventory.ReservedQuantity += quantityRequested;
                        _warehouseInventoryService.Update(inventory, transaction);
                    }
                }

                //now adjust the fulfillments which were removed
                var fulfillmentsToRemove = savedFulfillments.Where(x => !warehouseIds.Contains(x.WarehouseId)).ToList();
                if (fulfillmentsToRemove.Any())
                {
                    //get the saved inventories
                    var rOrderItemIds = fulfillmentsToRemove.Select(x => x.OrderItemId).ToList();
                    var rOrderItems = orderItems.Where(x => rOrderItemIds.Contains(x.Id)).ToList();
                    foreach (var fRemove in fulfillmentsToRemove)
                    {
                        var rOrderItem = rOrderItems.First(x => x.Id == fRemove.OrderItemId);
                        var rInventory = rOrderItem.GetWarehouseInventory(fRemove.WarehouseId);
                        //update the inventory
                        rInventory.ReservedQuantity = rInventory.ReservedQuantity - fRemove.Quantity;
                        _warehouseInventoryService.Update(rInventory, transaction);
                        _orderFulfillmentService.Delete(fRemove, transaction);
                    }
                }
            });
            return !success ? R.Fail.Result : R.Success.Result;
        }

        #endregion

        #region Shipments

        [DualGet("{orderId}/shipments", Name = AdminRouteNames.ShipmentsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        public IActionResult ShipmentsList(int orderId)
        {
            if (orderId < 0 || _orderService.Count(x => x.Id == orderId) == 0)
                return NotFound();

            var fulfillments = _orderFulfillmentService.Get(x => x.OrderId == orderId).ToList();
            if (!fulfillments.Any())
                return R.Fail.With("error", T("No order fulfillments found")).Result;

            //get shipments
            var shipments = _shipmentService.GetShipmentsByOrderId(orderId);
            var shipmentModels = _shipmentModelFactory.Create(shipments);
            if (!shipmentModels.Any())
            {

            }
            return R.Success.With("orderId", orderId)
                .With("shipments", shipmentModels)
                .WithAvailableShipmentStatusTypes()
                .Result;
        }

        [DualGet("{orderId}/shipments/{shipmentId}/{warehouseId}", Name = AdminRouteNames.GetShipment)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        public IActionResult ShipmentEditor(int orderId, int shipmentId, int warehouseId)
        {
            Order order = null;
            if (orderId < 1 || (order = _orderService.Get(orderId)) == null)
                return NotFound();
            Warehouse warehouse;
            if (warehouseId < 1 || (warehouse = _warehouseService.Get(warehouseId)) == null)
                return NotFound();

            //get fulfillments
            var fulfillments = _orderFulfillmentService.Get(x => x.OrderId == orderId && x.WarehouseId == warehouseId).ToList();
            if (!fulfillments.Any())
                return R.Fail.With("error", T("No order fulfillments found")).Result;

            //get shipment
            var allShipments = _shipmentService.GetShipmentsByOrderId(orderId).Where(x => x.WarehouseId == warehouseId).ToList();
            var shipment = allShipments.FirstOrDefault(x => x.Id == shipmentId);
            if (shipmentId > 0 && shipment == null)
                return NotFound();
            shipment = shipment ?? new Shipment();
            var shipmentModel = _shipmentModelFactory.Create(shipment);
            if (shipment.Id == 0)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    //check other shipments which have this order item
                    var orderItemShipments = allShipments
                        .Where(x => x.ShipmentItems.Any(y => y.OrderItemId == orderItem.Id))
                        .ToList();

                    var quantityShipped = orderItemShipments.SelectMany(x => x.ShipmentItems).Sum(x => x.Quantity);
                    var quantityOrdered = fulfillments.First(x => x.OrderItemId == orderItem.Id).Quantity;
                    var remainingQuantities = quantityOrdered - quantityShipped;
                    if (remainingQuantities < 0)
                        remainingQuantities = 0;
                    shipmentModel.ShipmentItems.Add(new ShipmentItemModel()
                    {
                        ProductName = orderItem.Product.Name,
                        OrderedQuantity = quantityOrdered,
                        ShippedQuantity = quantityShipped,
                        Quantity = remainingQuantities,
                        OrderItemId = orderItem.Id,
                        AttributeText = _formatterService.FormatProductAttributes(orderItem.AttributeJson)
                    });

                }
            }
            shipmentModel.OrderId = orderId;
            var warehouseModel = _warehouseModelFactory.CreateMini(warehouse);
            var shipmentItemsCount = shipmentModel.ShipmentItems.Sum(x => x.Quantity);
            return R.Success.With("shipment", shipmentModel).With("orderId", orderId).With("warehouse", warehouseModel)
                .With("shipmentItemsCount", shipmentItemsCount).Result;
        }

        [DualPost("{orderId}/shipments", Name = AdminRouteNames.SaveShipment, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        [ValidateModelState(ModelType = typeof(ShipmentModel))]
        public IActionResult SaveShipment(ShipmentModel shipmentModel)
        {
            var shipment = shipmentModel.Id > 0 ? _shipmentService.Get(shipmentModel.Id) : new Shipment();
            if (shipment == null)
                return NotFound();
            Order order = null;
            if (shipmentModel.OrderId == 0 || shipmentModel.ShipmentItems == null ||
                (order = _orderService.Get(shipmentModel.OrderId)) == null)
                return NotFound();

            if (shipmentModel.ShipmentItems.Sum(x => x.Quantity) == 0)
                return R.Fail.With("error", T("At least one item must be there to create a shipment")).Result;

            var passedOrderItemIds = shipmentModel.ShipmentItems.Select(x => x.OrderItemId).ToList();
            var orderOrderItemIds = order.OrderItems.Select(x => x.Id).ToList();
            if (passedOrderItemIds.Except(orderOrderItemIds).Any())
                return R.Fail.With("error", T("Invalid order item identifier was passed")).Result;

            //is a valid warehouse supplied?
            if (shipmentModel.WarehouseId < 1)
            {
                //assign a default warehouse
                var warehouse = _warehouseService.Get(out _, x => true, x => x.DisplayOrder).FirstOrDefault();
                if (warehouse == null)
                    return R.Fail.With("error", T("No warehouse found for shipment")).Result;
                shipmentModel.WarehouseId = warehouse.Id;
            }
            else
            {
                if (_warehouseService.Count(x => x.Id == shipmentModel.WarehouseId) == 0)
                {
                    return R.Fail.With("error", T("No warehouse found for shipment")).Result;
                }
            }
            //check if there is an orderfulfillment
            var fulfillments = _orderFulfillmentService.Get(x =>
                x.OrderId == shipmentModel.OrderId && x.WarehouseId == shipmentModel.WarehouseId).ToList();
            if (!fulfillments.Any())
                return R.Fail.With("error", T("The warehouse doesn't have any matching fulfillments")).Result;

            //very that store is not shipping more items than fulfilled
            foreach (var fulfillment in fulfillments)
            {
                var orderItem = shipmentModel.ShipmentItems.FirstOrDefault(x => x.OrderItemId == fulfillment.OrderItemId);
                if (orderItem == null)
                    continue;
                if (fulfillment.Quantity < orderItem.Quantity)
                {
                    return R.Fail.With("error", T("Can't ship more items than fulfilled")).Result;
                }
            }
            shipment.TrackingNumber = shipmentModel.TrackingNumber;
            shipment.Remarks = shipmentModel.Remarks;
            shipment.ShipmentStatus = shipmentModel.ShipmentStatus;
            shipment.ShippingMethodName = shipmentModel.ShippingMethodName;
            if (shipment.Id == 0)
            {
                shipment.WarehouseId = shipmentModel.WarehouseId;
                _shipmentService.Insert(shipment);

                //add shipment items
                foreach (var shipmentItemModel in shipmentModel.ShipmentItems)
                {
                    var orderItem = order.OrderItems.FirstOrDefault(x => x.Id == shipmentItemModel.OrderItemId);
                    if (orderItem == null)
                        continue;
                    var shipmentItem = new ShipmentItem()
                    {
                        OrderItem = orderItem,
                        Shipment = shipment,
                        ShipmentId = shipment.Id,
                        OrderItemId = orderItem.Id,
                        Quantity = shipmentItemModel.Quantity
                    };
                    _shipmentItemService.Insert(shipmentItem);
                }
            }
            else
            {
                //add shipment items
                foreach (var shipmentItemModel in shipmentModel.ShipmentItems)
                {
                    var shipmentItem =
                        shipment.ShipmentItems.FirstOrDefault(x => x.OrderItemId == shipmentItemModel.OrderItemId);
                    if (shipmentItem != null && shipmentItem.Quantity != shipmentItemModel.Quantity)
                    {
                        shipmentItem.Quantity = shipmentItemModel.Quantity;
                        _shipmentItemService.Update(shipmentItem);
                    }
                }
                _shipmentService.Update(shipment);
            }

            return R.Success.Result;
        }

        [DualPost("{orderId}/shipments/{shipmentId}", Name = AdminRouteNames.DeleteShipment, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        public IActionResult DeleteShipment(int orderId, int shipmentId)
        {
            if (orderId == 0 || _orderService.Count(x => x.Id == orderId) == 0)
                return NotFound();

            //get shipment
            var allShipments = _shipmentService.GetShipmentsByOrderId(orderId);
            var shipment = allShipments.FirstOrDefault(x => x.Id == shipmentId);
            if (shipmentId > 0 && shipment == null)
                return NotFound();
            _shipmentService.Delete(shipment);
            return R.Success.Result;
        }

        [DualPost("{orderId}/shipment-history", Name = AdminRouteNames.SaveShipmentHistory, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        [ValidateModelState(ModelType = typeof(ShipmentHistoryModel))]
        public IActionResult SaveShipmentHistory(ShipmentHistoryModel shipmentHistoryModel)
        {
            //find the shipment history
            var shipmentHistory = shipmentHistoryModel.Id > 0 ? _shipmentStatusHistoryService.Get(shipmentHistoryModel.Id) : new ShipmentHistory()
            {
                ShipmentId = shipmentHistoryModel.ShipmentId
            };
            if (shipmentHistory == null)
                return NotFound();
            //get the shipment and update it
            var shipment = _shipmentService.Get(shipmentHistoryModel.ShipmentId);
            if (shipment == null)
                return NotFound();

            shipmentHistory.CreatedOn = shipmentHistory.Id == 0 ? DateTime.UtcNow : shipmentHistoryModel.CreatedOn;
            if (shipmentHistory.Id == 0)
            {
                shipmentHistory.ShipmentStatus = shipmentHistoryModel.ShipmentStatus;
                //do we need to update the shipment status?
                if (shipment.ShipmentStatus != shipmentHistoryModel.ShipmentStatus)
                {
                    shipment.ShipmentStatus = shipmentHistoryModel.ShipmentStatus;
                    _shipmentService.Update(shipment);

                    //raise event
                    switch (shipment.ShipmentStatus)
                    {
                        case ShipmentStatus.InTransit:
                            RaiseEvent(NamedEvent.ShipmentShipped, shipment);
                            break;
                        case ShipmentStatus.Delivered:
                            RaiseEvent(NamedEvent.ShipmentDelivered, shipment);
                            break;
                        case ShipmentStatus.DeliveryFailed:
                            RaiseEvent(NamedEvent.ShipmentDeliveryFailed, shipment);
                            break;
                    }
                }
            }
            else
            {
                _shipmentStatusHistoryService.Update(shipmentHistory);
            }
            return R.Success.Result;
        }

        [DualPost("{orderId}/shipment-history/delete", Name = AdminRouteNames.DeleteShipmentHistory, OnlyApi = true)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        [ValidateModelState(ModelType = typeof(ShipmentHistoryModel))]
        public IActionResult DeleteShipmentHistory(ShipmentHistoryModel shipmentHistoryModel)
        {
            //find the shipment history
            var shipmentHistory = _shipmentStatusHistoryService.Get(shipmentHistoryModel.Id);
            if (shipmentHistory == null)
                return NotFound();

            _shipmentStatusHistoryService.Delete(shipmentHistory);
            return R.Success.Result;
        }

        #endregion

        #region Return Requests

        [DualGet("return-requests", Name = AdminRouteNames.ReturnRequestsList)]
        [CapabilityRequired(CapabilitySystemNames.ViewReturnRequests)]
        public IActionResult ReturnRequestsList(ReturnRequestSearchModel searchModel)
        {
            var returnRequests = _returnRequestService.GetWithOrderDetails(out int totalResults, searchModel.ReturnRequestStatus, searchModel.FromDate, searchModel.ToDate, searchModel.Current,
                searchModel.RowCount);

            var returnRequestsModel = returnRequests.Select(_returnRequestModelFactory.Create).ToList();
            return R.Success.With("returnRequests",  returnRequestsModel)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .Result;
        }

        [DualGet("return-requests/{returnRequestId}", Name = AdminRouteNames.GetReturnRequest)]
        [CapabilityRequired(CapabilitySystemNames.EditReturnRequest)]
        public IActionResult ReturnRequestEditor(int returnRequestId)
        {
            var returnRequest = returnRequestId > 0 ? _returnRequestService.Get(returnRequestId) : null;
            if (returnRequest == null)
                return NotFound();
            var model = _returnRequestModelFactory.Create(returnRequest);
            var availableReturnStatusValues = SelectListHelper.GetSelectItemList<ReturnRequestStatus>();
            var availableReturnOptionValues = SelectListHelper.GetSelectItemList<ReturnOption>();
            return R.Success.With("returnRequest", model)
                .With("availableReturnStatusValues", availableReturnStatusValues)
                .With("availableReturnOptionValues", availableReturnOptionValues).Result;
        }

        [DualPost("return-requests", Name = AdminRouteNames.SaveReturnRequest)]
        [CapabilityRequired(CapabilitySystemNames.EditReturnRequest)]
        [ValidateModelState(ModelType = typeof(ReturnRequestModel))]
        public IActionResult SaveReturnRequest(ReturnRequestModel returnRequestModel)
        {
            var returnRequest = returnRequestModel.Id > 0 ? _returnRequestService.Get(returnRequestModel.Id) : null;
            if (returnRequest == null)
                return NotFound();

            switch (returnRequestModel.ReturnOption)
            {
                case ReturnOption.ReturnRepaired:
                case ReturnOption.ReturnFresh:
                    //if the return order was already created, don't do anything
                    if (!returnRequest.ReturnOrderId.HasValue)
                    {
                        //get the original items to check for available inventory
                        var orderItem = _orderItemService.GetWithProducts(new List<int>() {returnRequest.OrderItemId}).FirstOrDefault();
                        if (orderItem == null)
                            return R.Fail.With("error", T("An error occured while processing return")).Result;
                        if (!orderItem.IsAvailableInStock(returnRequest.Quantity))
                            return R.Fail.With("error", T("The item is not available in any of the warehouses")).Result;
                        
                        var clonedOrder = _orderAccountant.CloneOrder(orderItem.Order);
                        clonedOrder.OrderItems[0].Quantity = returnRequest.Quantity;
                        clonedOrder.OrderItems[0].Price = 0m;
                        clonedOrder.OrderTotal = 0;
                        clonedOrder.Subtotal = 0;
                        clonedOrder.PaymentStatus = PaymentStatus.Complete;
                        _orderAccountant.InsertCompleteOrder(clonedOrder);

                        //save return order id
                        returnRequest.ReturnOrderId = clonedOrder.Id;

                        //update the status of existing order
                        _purchaseAccountant.EvaluateOrderStatus(orderItem.Order.Guid);
                    }
                    break;
                case ReturnOption.Refund:
                    break;
                case ReturnOption.Other:
                    //do nothing
                    break;
            }
            returnRequest.ReturnRequestStatus = returnRequestModel.ReturnRequestStatus;
            returnRequest.UpdatedOn = DateTime.UtcNow;
            returnRequest.Remarks = returnRequestModel.Remarks;
            returnRequest.StaffComments = returnRequestModel.StaffComments;
            returnRequest.ReturnOption = returnRequestModel.ReturnOption;
            _returnRequestService.Update(returnRequest);
            return R.Success.With("id", returnRequest.Id).Result;
        }

        [DualPost("return-requests/delete", Name = AdminRouteNames.DeleteReturnRequest)]
        [CapabilityRequired(CapabilitySystemNames.EditReturnRequest)]
        public IActionResult DeleteReturnRequest(int returnRequestId)
        {
            var returnRequest = returnRequestId > 0 ? _returnRequestService.Get(returnRequestId) : new ReturnRequest();
            if (returnRequest == null)
                return NotFound();

            _returnRequestService.Delete(returnRequest);
            return R.Success.Result;
        }
        #endregion
    }
}