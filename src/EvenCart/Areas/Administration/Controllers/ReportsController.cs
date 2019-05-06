using System;
using System.Linq;
using EvenCart.Areas.Administration.Factories.Reports;
using EvenCart.Areas.Administration.Models.Reports;
using EvenCart.Data.Constants;
using EvenCart.Data.Enum;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Services.Products;
using EvenCart.Services.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Areas.Administration.Controllers
{
    [CapabilityRequired(CapabilitySystemNames.ManageReports)]
    public class ReportsController : FoundationAdminController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IReportModelFactory _reportModelFactory;
        public ReportsController(IProductService productService, IReportModelFactory reportModelFactory, IOrderService orderService)
        {
            _productService = productService;
            _reportModelFactory = reportModelFactory;
            _orderService = orderService;
        }

        [DualGet("stock-report", Name = AdminRouteNames.StockReport)]
        public IActionResult StockReport(StockReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new StockReportSearchModel();

            var products = _productService.GetProductsWithVariants(out int totalResults, searchModel.ProductSearch,
                searchModel.Published, true, x => x.StockQuantity,
                SortOrder.Ascending,
                page: searchModel.Current, count: searchModel.RowCount);


            var models = products.Select(_reportModelFactory.Create).ToList();
            return R.Success.WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount)
                .With("reportItems", models)
                .Result;
        }

        [DualGet("orders-report", Name = AdminRouteNames.OrdersReport)]
        public IActionResult OrdersReport(OrderReportSearchModel searchModel)
        {
            searchModel = searchModel ?? new OrderReportSearchModel();
            searchModel.EndDate = searchModel.EndDate ?? DateTime.UtcNow;
            searchModel.StartDate = searchModel.StartDate ?? searchModel.EndDate.Value.AddDays(-29);
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase,
                orderStatus: searchModel.OrderStatus, paymentStatus: searchModel.PaymentStatus,
                startDate: searchModel.StartDate, endDate: searchModel.EndDate).ToList();

            var models =
                _reportModelFactory.CreateOrderReportModels( out int totalResults, orders, searchModel.GroupBy, searchModel.Current, searchModel.RowCount);
            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
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
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase,
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
            var orders = _orderService.GetOrders(out _, searchModel.SearchPhrase, startDate: searchModel.StartDate, endDate: searchModel.EndDate).ToList();

            var models =
                _reportModelFactory.CreateProductOrderReportModels(out int totalResults, orders, searchModel.Current, searchModel.RowCount);
            return R.Success.With("reportItems", models)
                .With("startDate", searchModel.StartDate)
                .With("endDate", searchModel.EndDate)
                .WithAvailableOrderStatusTypes()
                .WithAvailablePaymentStatusTypes()
                .WithGridResponse(totalResults, searchModel.Current, searchModel.RowCount).Result;

        }
    }
}