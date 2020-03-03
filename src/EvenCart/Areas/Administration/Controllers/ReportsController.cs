using System;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Orders;
using EvenCart.Areas.Administration.Factories.Reports;
using EvenCart.Areas.Administration.Factories.Users;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Users;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using EvenCart.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    [CapabilityRequired(CapabilitySystemNames.ManageReports)]
    public class ReportsController : FoundationAdminController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IReportModelFactory _reportModelFactory;
        private readonly IUserService _userService;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IUserModelFactory _userModelFactory;
        private readonly IRoleService _roleService;
        private readonly IWarehouseService _warehouseService;
        public ReportsController(IProductService productService, IReportModelFactory reportModelFactory, IOrderService orderService, IUserService userService, IOrderModelFactory orderModelFactory, IUserModelFactory userModelFactory, IRoleService roleService, IWarehouseService warehouseService)
        {
            _productService = productService;
            _reportModelFactory = reportModelFactory;
            _orderService = orderService;
            _userService = userService;
            _orderModelFactory = orderModelFactory;
            _userModelFactory = userModelFactory;
            _roleService = roleService;
            _warehouseService = warehouseService;
        }

        [DualGet("stock-report", Name = AdminRouteNames.StockReport)]
        public IActionResult StockReport(StockReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new StockReportSearchModel();
            //get availalbe warehouses
            var warehouses = _warehouseService.Get(x => true).ToList();
            if (searchModel.WarehouseId < 1)
                searchModel.WarehouseId = warehouses.First().Id;
            var products = _productService.GetProductsWithVariants(out int totalResults, searchModel.WarehouseId, searchModel.ProductSearch,
                searchModel.Published, true, page: searchModel.Current, count: searchModel.RowCount);


            var models = products.Select(_reportModelFactory.Create).ToList();
           
            var warehouseModels = SelectListHelper.GetSelectItemListWithAction(warehouses, x => x.Id, x => x.Address.Name);
            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("reportItems", models)
                .With("availableWarehouses", warehouseModels)
                .Result;
        }

        [DualGet("orders-report", Name = AdminRouteNames.OrdersReport)]
        public IActionResult OrdersReport(OrderReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new OrderReportSearchModel();
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.StartDate = searchModel.StartDate ?? searchModel.EndDate.Value.AddDays(-29);
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase, storeId: CurrentStore.Id,
                orderStatus: searchModel.OrderStatus, paymentStatus: searchModel.PaymentStatus,
                startDate: searchModel.StartDate, endDate: searchModel.EndDate).ToList();

            var models =
                _reportModelFactory.CreateOrderReportModels(out int totalResults, orders, searchModel.GroupBy, searchModel.Current, searchModel.RowCount);
            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
                .With("groupBy", searchModel.GroupBy)
                .WithAvailableOrderStatusTypes()
                .WithAvailablePaymentStatusTypes()
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;

        }

        [DualGet("user-orders-report", Name = AdminRouteNames.UserOrdersReport)]
        public IActionResult UserOrdersReport(OrderReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new OrderReportSearchModel();
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.StartDate = searchModel.StartDate ?? DateTime.UtcNow.AddDays(-29);
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase, storeId: CurrentStore.Id,
                orderStatus: searchModel.OrderStatus, paymentStatus: searchModel.PaymentStatus,
                startDate: searchModel.StartDate, endDate: searchModel.EndDate).ToList();

            var models =
                _reportModelFactory.CreateUserOrderReportModels(out int totalResults, orders, searchModel.Current, searchModel.RowCount);
            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
                .WithAvailableOrderStatusTypes()
                .WithAvailablePaymentStatusTypes()
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;

        }

        [DualGet("products-report", Name = AdminRouteNames.ProductOrdersReport)]
        public IActionResult ProductOrdersReport(ProductReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new ProductReportSearchModel();
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.StartDate = searchModel.StartDate ?? DateTime.UtcNow.AddDays(-29);
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase, storeId: CurrentStore.Id, startDate: searchModel.StartDate, endDate: searchModel.EndDate).ToList();

            var models =
                _reportModelFactory.CreateProductOrderReportModels(out int totalResults, orders, searchModel.Current, searchModel.RowCount);
            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
                .WithAvailableOrderStatusTypes()
                .WithAvailablePaymentStatusTypes()
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;

        }
        /// <summary>
        /// Gets totals for various entities
        /// </summary>
        /// <response code="200">A <see cref="TotalsReportModel">totals</see> object</response>
        [DualGet("totals", Name = AdminRouteNames.TotalsReport)]
        public IActionResult TotalsReport()
        {
            var response = R.Success;
            var orderCounts = _orderService.GetOrderCountsByStatus(CurrentStore.Id);
            var model = new TotalsReportModel
            {
                TotalUsers = _userService.Count(x => !x.Deleted),
                TotalPaidAmount = _orderService.Get(x => x.PaymentStatus == PaymentStatus.Complete)
                    .Sum(x => x.OrderTotal),
                TotalCancelledOrders = orderCounts[OrderStatus.Cancelled],
                TotalNewOrders = orderCounts[OrderStatus.New],
                TotalClosedOrders = orderCounts[OrderStatus.Closed],
                TotalCompleteOrders = orderCounts[OrderStatus.Complete],
                TotalOnholdOrders = orderCounts[OrderStatus.OnHold],
                TotalDelayedOrders = orderCounts[OrderStatus.Delayed],
                TotalProcessingOrders = orderCounts[OrderStatus.Processing],
                TotalReturnedOrders = orderCounts[OrderStatus.Returned],
                TotalPartiallyShippedOrders = orderCounts[OrderStatus.PartiallyShipped],
                TotalShippedOrders = orderCounts[OrderStatus.Shipped],
                TotalOrders = orderCounts.Sum(x => x.Value)
            };

            return response.With("totals", model).Result;
        }

        /// <summary>
        /// Gets most recent 5 orders
        /// </summary>
        /// <response code="200">A list of <see cref="OrderModel">order</see> objects as 'orders'</response>
        [DualGet("recent-orders", Name = AdminRouteNames.RecentOrdersReport)]
        public IActionResult RecentOrdersReport()
        {
            var recentOrders = _orderService.GetOrders(out int _, storeId: CurrentStore.Id, count: 5);
            var orderModels = recentOrders.Select(_orderModelFactory.Create).ToList();
            return R.Success.With("orders", orderModels).Result;
        }

        /// <summary>
        /// Gets most recent 5 user registrations
        /// </summary>
        /// <response code="200">A list of <see cref="UserModel">user</see> objects as 'users'</response>
        [DualGet("recent-users", Name = AdminRouteNames.RecentUsersReport)]
        public IActionResult RecentUsersReport()
        {
            var guestRole = _roleService.FirstOrDefault(x => x.SystemName == SystemRoleNames.Visitor);

            var users = _userService.GetUsers(null, new[] { guestRole.Id }, x => x.CreatedOn, SortOrder.Descending, 1,
                5, out _, true);
            var userModels = users.Select(_userModelFactory.Create).ToList();
            return R.Success.With("users", userModels).Result;
        }

        /// <summary>
        /// Gets 5 best sellers products for last year
        /// </summary>
        /// <response code="200">A list of <see cref="ProductOrderReportModel">product</see> objects as 'products'</response>
        [DualGet("bestsellers", Name = AdminRouteNames.BestSellersReport)]
        public IActionResult BestSellersReport()
        {
            var orders = _orderService.GetOrders(out _, storeId: CurrentStore.Id, startDate: DateTime.UtcNow.AddDays(-365), count: 5).ToList();
            var models =
                _reportModelFactory.CreateProductOrderReportModels(out _, orders, 1, 5);
            return R.Success.With("products", models).Result;
        }

        /// <summary>
        /// Gets orders report based on provided date type 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="OrderReportModel">order report</see> objects as 'reportItems'</response>
        [DualGet("orders-chart-report", Name = AdminRouteNames.OrdersChartReport)]
        public IActionResult OrdersChartReport(OrderReportSearchModel searchModel)
        {
            if (searchModel.StartDate == null)
            {
                switch (searchModel.GroupBy)
                {
                    case GroupUnit.Days:
                        searchModel.StartDate = DateTime.UtcNow.AddDays(-29); //a month old
                        break;
                    case GroupUnit.Weeks:
                        searchModel.StartDate = new DateTime(DateTime.UtcNow.Year, 1, 1); //1st day of January of current year
                        break;
                    case GroupUnit.Months:
                        searchModel.StartDate = DateTime.UtcNow.AddYears(-1); //a year
                        break;
                    default:
                    case GroupUnit.Years:
                        searchModel.StartDate = DateTime.UtcNow.AddYears(-10); //a decade
                        break;
                }
            }
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.RowCount = int.MaxValue;
            searchModel.Current = 1;
            return OrdersReport(searchModel);
        }

        /// <summary>
        /// Gets orders report based on provided date type 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <response code="200">A list of <see cref="UserRegistrationReportModel">user report</see> objects as 'reportItems'</response>
        [DualGet("users-chart-report", Name = AdminRouteNames.UsersChartReport)]
        public IActionResult UsersChartReport(UsersReportSearchModel searchModel)
        {
            if (searchModel.StartDate == null)
            {
                switch (searchModel.GroupBy)
                {
                    case GroupUnit.Days:
                        searchModel.StartDate = DateTime.UtcNow.AddDays(-29); //a month old
                        break;
                    case GroupUnit.Weeks:
                        searchModel.StartDate = new DateTime(DateTime.UtcNow.Year, 1, 1); //1st day of January of current year
                        break;
                    case GroupUnit.Months:
                        searchModel.StartDate = DateTime.UtcNow.AddYears(-1); //a year
                        break;
                    default:
                    case GroupUnit.Years:
                        searchModel.StartDate = DateTime.UtcNow.AddYears(-10); //a decade
                        break;
                }
            }
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.RowCount = int.MaxValue;
            searchModel.Current = 1;
            var users = _userService.Get(out _,
                x => x.CreatedOn >= searchModel.StartDate && x.CreatedOn <= searchModel.EndDate).ToList();
            var models = _reportModelFactory.CreateUserRegistrationReportModels(out int totalResults, users,
                searchModel.GroupBy, searchModel.Current, searchModel.RowCount);

            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
                .With("groupBy", searchModel.GroupBy)
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;

        }
    }
}