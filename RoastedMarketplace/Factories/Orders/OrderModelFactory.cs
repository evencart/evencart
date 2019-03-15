using System.Linq;
using RoastedMarketplace.Data.Entity.Purchases;
using RoastedMarketplace.Data.Entity.Settings;
using RoastedMarketplace.Infrastructure.MediaServices;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Models.Addresses;
using RoastedMarketplace.Models.Orders;
using RoastedMarketplace.Services.Formatter;

namespace RoastedMarketplace.Factories.Orders
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IFormatterService _formatterService;
        private readonly TaxSettings _taxSettings;

        public OrderModelFactory(IModelMapper modelMapper, IMediaAccountant mediaAccountant, IFormatterService formatterService, TaxSettings taxSettings)
        {
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _formatterService = formatterService;
            _taxSettings = taxSettings;
        }

        public OrderModel Create(Order order)
        {
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
            model.OrderItems = order.OrderItems?.Select(Create).ToList();
            return model;   
        }

        public OrderItemModel Create(OrderItem orderItem)
        {
            var orderItemModel = _modelMapper.Map<OrderItemModel>(orderItem);
            orderItemModel.ProductName = orderItem.Product.Name;
            orderItemModel.ImageUrl = _mediaAccountant.GetPictureUrl(orderItem.Product.MediaItems?.FirstOrDefault(),
                returnDefaultIfNotFound: true);
            orderItemModel.AttributeText = _formatterService.FormatProductAttributes(orderItem.AttributeJson);
            orderItemModel.Price = _taxSettings.DisplayProductPricesWithoutTax
                ? orderItemModel.Price
                : orderItemModel.Price + orderItemModel.Tax / orderItemModel.Quantity;
            orderItemModel.TotalPrice = _taxSettings.DisplayProductPricesWithoutTax
                ? orderItem.Price * orderItem.Quantity
                : orderItem.Price * orderItem.Quantity + orderItem.Tax;
            orderItemModel.SeName = orderItem.Product.SeoMeta.Slug;
            orderItemModel.ShipmentStatus = orderItem.Shipment?.ShipmentStatus ?? ShipmentStatus.Preparing;
            return orderItemModel;
        }
    }
}