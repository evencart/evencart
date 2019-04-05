using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Areas.Administration.Factories.Orders;
using RoastedMarketplace.Areas.Administration.Models.Orders;
using RoastedMarketplace.Areas.Administration.Models.Users;
using RoastedMarketplace.Data.Constants;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Infrastructure.Security.Attributes;
using RoastedMarketplace.Services.Formatter;
using RoastedMarketplace.Services.Purchases;
using RoastedMarketplace.Services.Serializers;
using RoastedMarketplace.Services.Shipping;

namespace RoastedMarketplace.Areas.Administration.Controllers
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
        public OrdersController(IOrderService orderService, IModelMapper modelMapper, IDataSerializer dataSerializer, IFormatterService formatterService, IShipmentService shipmentService, IShipmentItemService shipmentItemService, IShipmentStatusHistoryService shipmentStatusHistoryService, IOrderModelFactory orderModelFactory)
        {
            _orderService = orderService;
            _modelMapper = modelMapper;
            _dataSerializer = dataSerializer;
            _formatterService = formatterService;
            _shipmentService = shipmentService;
            _shipmentItemService = shipmentItemService;
            _shipmentStatusHistoryService = shipmentStatusHistoryService;
            _orderModelFactory = orderModelFactory;
        }

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
        [DualGet("{orderId}/shipments", Name = AdminRouteNames.ShipmentsList)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        public IActionResult ShipmentsList(int orderId)
        {
            if (orderId == 0 || _orderService.Count(x => x.Id == orderId) == 0)
                return NotFound();

            //get shipments
            var shipments = _shipmentService.GetShipmentsByOrderId(orderId);
            var shipmentModels = shipments.Select(x =>
            {
                var shipmentModel = _modelMapper.Map<ShipmentModel>(x);
                shipmentModel.ShipmentItems = x.ShipmentItems.Select(y =>
                    {
                        var shipmentItemModel = _modelMapper.Map<ShipmentItemModel>(y);
                        shipmentItemModel.ProductName = y.OrderItem.Product.Name;
                        shipmentItemModel.Quantity = y.Quantity;
                        shipmentItemModel.OrderedQuantity = y.OrderItem.Quantity;
                        shipmentItemModel.AttributeText =
                            _formatterService.FormatProductAttributes(y.OrderItem.AttributeJson);
                        return shipmentItemModel;
                    })
                    .ToList();

                shipmentModel.ShipmentHistoryItems = x.ShipmentStatusHistories?.Select(y =>
                    {
                        var historyModel = _modelMapper.Map<ShipmentHistoryModel>(y);
                        return historyModel;
                    })
                    .ToList();
                return shipmentModel;
            }).ToList();

            return R.Success.WithGridResponse(shipmentModels.Count, 1, shipmentModels.Count)
                .With("orderId", orderId)
                .With("shipments", shipmentModels)
                .WithAvailableShipmentStatusTypes()
                .Result;
        }

        [DualGet("{orderId}/shipments/{shipmentId}", Name = AdminRouteNames.GetShipment)]
        [CapabilityRequired(CapabilitySystemNames.ManageShipment)]
        public IActionResult ShipmentEditor(int orderId, int shipmentId)
        {
            Order order = null;
            if (orderId == 0 || (order = _orderService.Get(orderId)) == null)
                return NotFound();

            //get shipment
            var allShipments = _shipmentService.GetShipmentsByOrderId(orderId);
            var shipment = allShipments.FirstOrDefault(x => x.Id == shipmentId);
            if (shipmentId > 0 && shipment == null)
                return NotFound();
            shipment = shipment ?? new Shipment();
            var shipmentModel = _modelMapper.Map<ShipmentModel>(shipment);
            shipmentModel.ShipmentItems = shipment.ShipmentItems?.Select(y =>
                                              {
                                                  var shipmentItemModel = _modelMapper.Map<ShipmentItemModel>(y);
                                                  shipmentItemModel.ProductName = y.OrderItem.Product.Name;
                                                  shipmentItemModel.OrderedQuantity = y.OrderItem.Quantity;
                                                  shipmentItemModel.Quantity = y.Quantity;
                                                  shipmentItemModel.AttributeText =
                                                      _formatterService.FormatProductAttributes(y.OrderItem
                                                          .AttributeJson);
                                                  return shipmentItemModel;
                                              })
                                              .ToList() ?? new List<ShipmentItemModel>();

            shipmentModel.ShipmentHistoryItems = shipment.ShipmentStatusHistories?.Select(x =>
                {
                    var historyModel = _modelMapper.Map<ShipmentHistoryModel>(x);
                    return historyModel;
                })
                .ToList();

            if (shipment.Id == 0)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    //check other shipments which have this order item
                    var orderItemShipments = allShipments
                        .Where(x => x.ShipmentItems.Any(y => y.OrderItemId == orderItem.Id))
                        .ToList();

                    var quantityShipped = orderItemShipments.SelectMany(x => x.ShipmentItems).Sum(x => x.Quantity);
                    var quantityOrdered = orderItem.Quantity;
                    shipmentModel.ShipmentItems.Add(new ShipmentItemModel() {
                        ProductName = orderItem.Product.Name,
                        OrderedQuantity = orderItem.Quantity,
                        ShippedQuantity = quantityShipped,
                        Quantity = quantityOrdered - quantityShipped,
                        OrderItemId = orderItem.Id,
                        AttributeText = _formatterService.FormatProductAttributes(orderItem.AttributeJson)
                    });

                }
            }

            shipmentModel.OrderId = orderId;

            return R.Success.With("shipment", shipmentModel).Result;
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

            shipment.TrackingNumber = shipmentModel.TrackingNumber;
            shipment.Remarks = shipmentModel.Remarks;
            shipment.ShipmentStatus = shipmentModel.ShipmentStatus;
            shipment.ShippingMethodName = shipmentModel.ShippingMethodName;

            if (shipment.Id == 0)
            {
                _shipmentService.Insert(shipment);

                //add shipment items
                foreach (var shipmentItemModel in shipmentModel.ShipmentItems)
                {
                    if (order.OrderItems.Any(x => x.Id == shipmentItemModel.OrderItemId))
                        _shipmentService.AddShipmentItem(shipment.Id, shipmentItemModel.OrderItemId, shipmentItemModel.Quantity);
                }
            }
            else
            {
                _shipmentService.Update(shipment);
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
            var shipmentHistory = shipmentHistoryModel.Id > 0 ? _shipmentStatusHistoryService.Get(shipmentHistoryModel.Id) : new ShipmentHistory() {
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

    }
}