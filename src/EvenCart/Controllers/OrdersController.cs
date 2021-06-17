﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Areas.Administration.Helpers;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Events;
using EvenCart.Factories.Orders;
using EvenCart.Factories.Products;
using EvenCart.Models.Orders;
using EvenCart.Models.Products;
using EvenCart.Services.Orders;
using EvenCart.Services.Products;
using EvenCart.Services.Shipping;
using Genesis.Helpers;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Meta;
using Genesis.Modules.Pdf;
using Genesis.Routing;
using Genesis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated users to manage orders
    /// </summary>
    [Authorize]
    [Route("orders")]
    public class OrdersController : GenesisController
    {
        private const string CancellationReasonLabel = "cancellationReason";
        private const string ReturnReasonLabel = "returnReason";
        private const string ReturnActionLabel = "returnAction";

        private readonly IOrderService _orderService;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly ICustomLabelService _customLabelService;
        private readonly OrderSettings _orderSettings;
        private readonly IShipmentStatusHistoryService _shipmentStatusHistoryService;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IReturnRequestModelFactory _requestModelFactory;
        private readonly IOrderAccountant _orderAccountant;
        private readonly IPdfService _pdfService;
        private readonly IDownloadService _downloadService;
        private readonly IOrderItemDownloadService _orderItemDownloadService;
        private readonly IProductModelFactory _productModelFactory;
        public OrdersController(IOrderService orderService, IOrderModelFactory orderModelFactory, ICustomLabelService customLabelService, OrderSettings orderSettings, IShipmentStatusHistoryService shipmentStatusHistoryService, IReturnRequestService returnRequestService, IReturnRequestModelFactory requestModelFactory, IOrderAccountant orderAccountant, IPdfService pdfService, IDownloadService downloadService, IOrderItemDownloadService orderItemDownloadService, IProductModelFactory productModelFactory)
        {
            _orderService = orderService;
            _orderModelFactory = orderModelFactory;
            _customLabelService = customLabelService;
            _orderSettings = orderSettings;
            _shipmentStatusHistoryService = shipmentStatusHistoryService;
            _returnRequestService = returnRequestService;
            _requestModelFactory = requestModelFactory;
            _orderAccountant = orderAccountant;
            _pdfService = pdfService;
            _downloadService = downloadService;
            _orderItemDownloadService = orderItemDownloadService;
            _productModelFactory = productModelFactory;
        }

        /// <summary>
        /// Gets the order with provided order identifier
        /// </summary>
        /// <param name="orderGuid">The unique order identifier. It's a guid.</param>
        /// <response code="200">The <see cref="OrderModel">order</see> object</response>
        [DualGet("{orderGuid}", Name = RouteNames.SingleOrder)]
        public IActionResult Index(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id)
                return NotFound();
            var r = R;
            var model = _orderModelFactory.Create(order);

            var canCancel = CanCancelOrder(order);
            var canReturn = CanReturnOrder(order, out _, out var lastReturnDate);
            r.With("canCancel", canCancel);
            r.With("canReturn", canReturn);
            if (canReturn)
                r.With("lastReturnDate", lastReturnDate);

            //are there any active return requests
            var returnRequests = _returnRequestService.GetOrderReturnRequests(order.Id);
            var returnRequestModels = returnRequests.Select(_requestModelFactory.Create).ToList();

            //downloads
            if (order.OrderItems?.Any(x => x.IsDownloadable) ?? false)
            {
                var downloadableOrderItems = order.OrderItems.Where(x => x.IsDownloadable).ToList();
                var productIds = downloadableOrderItems.Select(x => x.ProductId).ToList();
                var downloads = _downloadService.GetWithoutBytes(x => productIds.Contains(x.ProductId) && x.Published).ToList();
                if (downloads.Any())
                {
                    var downloadIds = downloads.Select(x => x.Id).ToList();
                    var itemDownloads = _orderItemDownloadService.Get(x => downloadIds.Contains(x.DownloadId));
                    var downloadModels = new List<DownloadModel>();
                    foreach (var itemDownload in itemDownloads)
                    {
                        var download = downloads.FirstOrDefault(x => x.Id == itemDownload.DownloadId);
                        if (download == null)
                            continue;//this should not be hit unless somebody has messed up with database and deleted the data manually
                        var downloadModel = _productModelFactory.Create(download);
                        downloadModel.Active = itemDownload.Active;
                        downloadModels.Add(downloadModel);
                    }

                    r.With("canDownload", downloadModels.Any());
                    r.With("downloads", downloadModels.OrderBy(x => x.DisplayOrder).ToList());
                }
            }
            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Orders", RouteNames.AccountOrders);
            SetBreadcrumbToRoute(order.OrderNumber, RouteNames.SingleOrder, new { orderGuid }, localize: false);

            return r.Success.With("order", model).With("returnRequests", returnRequestModels).With("canDownloadInvoice", CanDownloadInvoice(order)).Result;
        }
        /// <summary>
        /// Gets orders for the authenticated user
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="OrderModel">orders</see> objects.</response>
        [DualGet("~/account/orders", Name = RouteNames.AccountOrders)]
        public IActionResult Orders(OrderSearchModel searchModel)
        {
            var orderStatus = new List<OrderStatus>();
            switch (searchModel.OrderStatus)
            {
                case "closed":
                    orderStatus.Add(OrderStatus.Closed);
                    orderStatus.Add(OrderStatus.Complete);
                    break;
                case "open":
                    orderStatus.Add(OrderStatus.New);
                    orderStatus.Add(OrderStatus.OnHold);
                    orderStatus.Add(OrderStatus.Shipped);
                    orderStatus.Add(OrderStatus.Processing);
                    orderStatus.Add(OrderStatus.Delayed);
                    orderStatus.Add(OrderStatus.PartiallyShipped);
                    break;
                case "returned":
                    orderStatus.Add(OrderStatus.Returned);
                    break;
                case "cancelled":
                    orderStatus.Add(OrderStatus.Cancelled);
                    break;
                default:
                    break;//do nothing we'll show all orders by default
            }

            var orders = _orderService.GetOrders(out int totalResults, userId: CurrentUser.Id, storeId: CurrentStore.Id, startDate: searchModel.FromDate, endDate: searchModel.ToDate,
                orderStatus: orderStatus, page: searchModel.Current, count: searchModel.RowCount);
            var orderModels = orders.Select(x => _orderModelFactory.Create(x)).ToList();

            return R.Success.With("orders", orderModels).Result;
        }

        /// <summary>
        /// Gets the cancellation reasons for the order
        /// </summary>
        /// <param name="orderGuid">The unique order identifier. It's a guid.</param>
        /// <response code="200">A success response object</response>
        [DualGet("{orderGuid}/cancel", Name = RouteNames.CancelOrder)]
        public IActionResult CancelOrder(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id)
                return NotFound();
            if (!CanCancelOrder(order))
            {
                return R.Fail.With("error", T("Unable to cancel the order")).Result;
            }

            var orderModel = _orderModelFactory.Create(order);

            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Orders", RouteNames.AccountOrders);
            SetBreadcrumbToRoute(order.OrderNumber, RouteNames.SingleOrder, new { orderGuid }, localize: false);
            SetBreadcrumbToRoute("Cancellation Request", RouteNames.CancelOrder);

            var cancellationReasons = _customLabelService.GetCustomLabels(CancellationReasonLabel, out _).ToList();
            var selectList = SelectListHelper.GetSelectItemList(cancellationReasons, x => x.Id, x => x.Text);
            return R.Success.With("availableReasons", selectList).With("order", orderModel).With("orderGuid", orderGuid).Result;
        }

        /// <summary>
        /// Cancels the order with provided order identifier
        /// </summary>
        /// <param name="orderGuid">The unique order identifier. It's a guid.</param>
        /// <param name="cancelReasonId">The reason id for cancellation</param>
        /// <response code="200">A success response object</response>
        [DualPost("{orderGuid}/cancel", Name = RouteNames.CancelOrder, OnlyApi = true)]
        public IActionResult CancelOrder(string orderGuid, int cancelReasonId)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id)
                return NotFound();
            if (!CanCancelOrder(order))
            {
                return R.Fail.With("error", T("Unable to cancel the order")).Result;
            }

            var reason = _customLabelService
                .FirstOrDefault(x => x.Type == CancellationReasonLabel && x.Id == cancelReasonId)?.Text;
            _orderAccountant.CancelOrder(order, reason);
            if (order.OrderStatus == OrderStatus.Cancelled)
                RaiseEvent(NamedEvent.OrderCancelled);
            return R.Success.Result;
        }
        /// <summary>
        /// Gets the order with provided order identifier
        /// </summary>
        /// <param name="orderGuid">The unique order identifier. It's a guid.</param>
        /// <response code="200">The <see cref="OrderModel">order</see> object</response>
        [DualGet("{orderGuid}/return", Name = RouteNames.ReturnOrder)]
        public IActionResult ReturnOrder(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id)
                return NotFound();
            if (!CanReturnOrder(order, out var returnableOrderItems, out var lastReturnDate))
                return R.Fail.With("error", T("The order is not eligible for returns")).Result;

            var models = returnableOrderItems.Select(x =>
            {
                var returnItemModel = new ReturnItemModel()
                {
                    OrderItem = _orderModelFactory.Create(x),
                    MinimumQuantityToReturn = x.Product.MinimumPurchaseQuantity
                };
                if (returnItemModel.MinimumQuantityToReturn == 0)
                    returnItemModel.MinimumQuantityToReturn = 1;
                return returnItemModel;
            }).ToList();
            order.OrderItems = null;
            var orderModel = _orderModelFactory.Create(order);
            //get the actions and reasons
            var customLabels =
                _customLabelService.Get(
                    new List<string>() { ReturnActionLabel, ReturnReasonLabel }, out _).ToList();
            var actions = customLabels.Where(x => x.Type == ReturnActionLabel).ToList();
            var reasons = customLabels.Where(x => x.Type == ReturnReasonLabel).ToList();
            var reasonsAsSelectList = SelectListHelper.GetSelectItemList(reasons, x => x.Id, x => x.Text);
            var actionsAsSelectList = SelectListHelper.GetSelectItemList(actions, x => x.Id, x => x.Text);
            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Orders", RouteNames.AccountOrders);
            SetBreadcrumbToRoute(order.OrderNumber, RouteNames.SingleOrder, new { orderGuid }, localize: false);
            SetBreadcrumbToRoute("Return Request", RouteNames.ReturnOrder);

            return R.Success.With("returnItems", models).With("availableReasons", reasonsAsSelectList)
                .With("availableActions", actionsAsSelectList).With("order", orderModel)
                .With("lastReturnDate", lastReturnDate).Result;
        }
        /// <summary>
        /// Creates a return request for an order
        /// </summary>
        /// <param name="orderGuid">The unique order identifier. It's a guid.</param>
        /// <param name="returnRequests">A list of <see cref="ReturnRequestModel">return request</see> objects</param>
        /// <response code="200">A success response object</response>
        [DualPost("{orderGuid}/return", Name = RouteNames.ReturnOrder, OnlyApi = true)]
        public IActionResult CreateReturn(string orderGuid, IList<ReturnRequestModel> returnRequests)
        {
            if (returnRequests == null || !returnRequests.Any(x => x.OrderItemId > 0))
                return BadRequest();
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id)
                return NotFound();
            if (!CanReturnOrder(order, out var returnableItems, out var lastReturnDate))
                return R.Fail.With("error", T("The order is not eligible for returns")).Result;

            var allowedReturnItemIds = returnableItems.Select(x => x.Id).ToList();
            returnRequests = returnRequests.Where(x => x.OrderItemId > 0).ToList();
            var requestedReturnItemIds = returnRequests.Select(x => x.OrderItemId).ToList();
            if (requestedReturnItemIds.Except(allowedReturnItemIds).Any())
                return R.Fail.With("error", T("Some passed order items can't be returned")).Result;

            var areValidQuantitiesPassed = returnRequests.All(x =>
            {
                var orderItem = returnableItems.First(y => y.Id == x.OrderItemId);
                //the quantity passed should be less than or equal to the ordered quantities and should be more than or equal to the minimum purchase quantities
                return orderItem.Quantity >= x.Quantity && orderItem.Product.MinimumPurchaseQuantity <= x.Quantity;
            });
            if (!areValidQuantitiesPassed)
                return R.Fail.With("error", T("Invalid return quantities supplied")).Result;
            //get the actions and reasons
            var customLabels =
                _customLabelService.Get(
                    new List<string>() { ReturnActionLabel, ReturnReasonLabel }, out _).ToList();
            var actions = customLabels.Where(x => x.Type == ReturnActionLabel).ToList();
            var reasons = customLabels.Where(x => x.Type == ReturnReasonLabel).ToList();
            var returnRequestList = new List<ReturnRequest>();
            //create the return request for each item now
            Transaction.Initiate(transaction =>
            {
                foreach (var returnRequest in returnRequests)
                {
                    var returnR = new ReturnRequest()
                    {
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        CustomerComments = returnRequest.CustomerComments,
                        OrderItemId = returnRequest.OrderItemId,
                        Quantity = returnRequest.Quantity,
                        ReturnRequestStatus = ReturnRequestStatus.Pending,
                        ReturnReason = reasons.FirstOrDefault(x => x.Id == returnRequest.ReturnReasonId)?.Text,
                        ReturnAction = actions.FirstOrDefault(x => x.Id == returnRequest.ReturnActionId)?.Text,
                        OrderId = order.Id,
                        UserId = CurrentUser.Id,
                        OrderItem = returnableItems.First(x => x.Id == returnRequest.OrderItemId),
                        Order = order,
                    };
                    _returnRequestService.Insert(returnR, transaction);
                    returnRequestList.Add(returnR);
                }
            });
            RaiseEvent(NamedEvent.ReturnRequestCreated, CurrentUser, order, returnRequestList);
            return R.Success.Result;
        }

        [HttpGet("{orderGuid}/invoice", Name = RouteNames.DownloadInvoice)]
        public IActionResult DownloadInvoice(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null || order.UserId != CurrentUser.Id || !CanDownloadInvoice(order))
                return NotFound();

            var pdfBytes = _pdfService.GetPdfBytes(PrintHelper.GetInvoice(order));
            return File(pdfBytes, "application/pdf", $"order_invoice_{order.Id}.pdf");
        }
        #region Helpers

        private bool CanCancelOrder(Order order)
        {
            return order.UserId == CurrentUser.Id &&
                   (_orderSettings.CancellationAllowedFor.Contains(order.OrderStatus.ToString())) &&
                   order.OrderItems.Any(x => x.Product.IsDownloadable && !x.Product.IsShippable);
        }

        private bool CanReturnOrder(Order order, out IList<OrderItem> orderItems, out DateTime lastReturnDate)
        {
            orderItems = new List<OrderItem>();
            lastReturnDate = DateTime.UtcNow;
            if (order.DisableReturns || order.OrderStatus != OrderStatus.Complete)
                return false;
            //get previous return requests for this order
            var returnRequests = _returnRequestService.Get(x => x.OrderId == order.Id).ToList();
            //check if there are ready return request available
            if (order.OrderStatus == OrderStatus.Complete)
            {
                //get the date of shipment that was delivered last
                //get the shipmenthistory
                var orderShipmentIds = order.Shipments.Select(x => x.Id).ToList();
                var historyItems = _shipmentStatusHistoryService.Get(x => orderShipmentIds.Contains(x.ShipmentId)).ToList();
                var deliveryDate = historyItems.Where(x => x.ShipmentStatus == ShipmentStatus.Delivered)
                    .Max(x => x.CreatedOn);

                //check if any order item is returnable
                foreach (var orderItem in order.OrderItems)
                {
                    if (!orderItem.Product.AllowReturns)
                        continue;
                    //are there any return requests created for this order item already?
                    if (returnRequests.Any(x => x.OrderItemId == orderItem.Id))
                        continue;
                    var allowReturnDays = orderItem.Product.DaysForReturn;
                    var passedDays = DateTime.UtcNow.Subtract(deliveryDate).Days;
                    if (allowReturnDays < passedDays)
                        continue;
                    orderItems.Add(orderItem);
                }

                lastReturnDate = orderItems.Any() ? deliveryDate.AddDays(orderItems.Max(x => x.Product.DaysForReturn)) : DateTime.UtcNow;
            }
            return orderItems.Any();
        }

        private bool CanDownloadInvoice(Order order)
        {
            return order.OrderStatus == OrderStatus.Complete && order.PaymentStatus == PaymentStatus.Complete;
        }
        #endregion
    }
}