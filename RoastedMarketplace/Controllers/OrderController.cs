using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Factories.Orders;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Addresses;
using RoastedMarketplace.Models.Orders;
using RoastedMarketplace.Services.Formatter;
using RoastedMarketplace.Services.Purchases;

namespace RoastedMarketplace.Controllers
{
    [Authorize]
    [Route("orders")]
    public class OrderController : FoundationController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderModelFactory _orderModelFactory;

        public OrderController(IOrderService orderService, IOrderModelFactory orderModelFactory)
        {
            _orderService = orderService;
            _orderModelFactory = orderModelFactory;
        }

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
    }
}