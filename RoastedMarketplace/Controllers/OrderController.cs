using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
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
        private readonly IModelMapper _modelMapper;
        private readonly IFormatterService _formatterService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly TaxSettings _taxSettings;
        public OrderController(IOrderService orderService, IModelMapper modelMapper, IFormatterService formatterService, IMediaAccountant mediaAccountant, TaxSettings taxSettings)
        {
            _orderService = orderService;
            _modelMapper = modelMapper;
            _formatterService = formatterService;
            _mediaAccountant = mediaAccountant;
            _taxSettings = taxSettings;
        }

        [DualGet("{orderGuid}", Name = RouteNames.SingleOrder)]
        public IActionResult Index(string orderGuid)
        {
            var order = _orderService.GetByGuid(orderGuid);
            if (order == null)
                return NotFound();
            var model = _modelMapper.Map<OrderModel>(order);
            model.BillingAddress = _modelMapper.Map<AddressInfoModel>(order.BillingAddress);
            model.BillingAddress.StateProvinceName =
                order.BillingAddress.StateOrProvince?.Name ?? order.BillingAddress.StateProvinceName;
            model.BillingAddress.CountryName = order.BillingAddress.Country.Name;

            if (order.ShippingAddress != null)
            {
                model.ShippingAddress = _modelMapper.Map<AddressInfoModel>(order.ShippingAddress);
                model.ShippingAddress.StateProvinceName =
                    order.ShippingAddress.StateOrProvince?.Name ?? order.ShippingAddress.StateProvinceName;
                model.ShippingAddress.CountryName = order.ShippingAddress.Country.Name;
            }
            model.OrderItems = order.OrderItems?.Select(x =>
                {
                    var orderItemModel = _modelMapper.Map<OrderItemModel>(x);
                    orderItemModel.ProductName = x.Product.Name;
                    orderItemModel.ImageUrl = _mediaAccountant.GetPictureUrl(x.Product.MediaItems?.FirstOrDefault(),
                        returnDefaultIfNotFound: true);
                    orderItemModel.AttributeText = _formatterService.FormatProductAttributes(x.AttributeJson);
                    orderItemModel.Price = _taxSettings.DisplayProductPricesWithoutTax
                        ? orderItemModel.Price
                        : orderItemModel.Price + orderItemModel.Tax / orderItemModel.Quantity;
                    orderItemModel.TotalPrice = _taxSettings.DisplayProductPricesWithoutTax
                        ? x.Price * x.Quantity
                        : x.Price * x.Quantity + x.Tax;
                    orderItemModel.SeName = x.Product.SeoMeta.Slug;
                    orderItemModel.ShipmentStatus = x.Shipment?.ShipmentStatus ?? ShipmentStatus.Preparing;
                    return orderItemModel;
                })
                .ToList();

            //set breadcrumb nodes
            SetBreadcrumbToRoute("Account", RouteNames.AccountProfile);
            SetBreadcrumbToRoute("Orders", RouteNames.AccountOrders);
            SetBreadcrumbToRoute(order.OrderNumber, RouteNames.SingleOrder, new { orderGuid }, localize: false);

            return R.Success.With("order", model).Result;
        }
    }
}