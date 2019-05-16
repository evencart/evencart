using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Purchases;
using EvenCart.Factories.Orders;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated users to manage orders
    /// </summary>
    [Authorize]
    [Route("orders")]
    public class OrdersController : FoundationController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderModelFactory _orderModelFactory;

        public OrdersController(IOrderService orderService, IOrderModelFactory orderModelFactory)
        {
            _orderService = orderService;
            _orderModelFactory = orderModelFactory;
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
            if (order == null)
                return NotFound();

            var model = _orderModelFactory.Create(order);

            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Orders", RouteNames.AccountOrders);
            SetBreadcrumbToRoute(order.OrderNumber, RouteNames.SingleOrder, new { orderGuid }, localize: false);

            return R.Success.With("order", model).Result;
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

            var orders = _orderService.GetOrders(out int totalResults, userId: CurrentUser.Id, startDate: searchModel.FromDate, endDate: searchModel.ToDate,
                orderStatus: orderStatus, page: searchModel.Current, count: searchModel.RowCount);
            var orderModels = orders.Select(x => _orderModelFactory.Create(x)).ToList();

            return R.Success.With("orders", orderModels).Result;
        }
    }
}