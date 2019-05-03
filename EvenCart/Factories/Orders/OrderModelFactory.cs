using System.Linq;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Services.Formatter;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Models.Addresses;
using EvenCart.Models.Orders;
using EvenCart.Services.Serializers;

namespace EvenCart.Factories.Orders
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IFormatterService _formatterService;
        private readonly TaxSettings _taxSettings;
        private readonly IDataSerializer _dataSerializer;

        public OrderModelFactory(IModelMapper modelMapper, IMediaAccountant mediaAccountant, IFormatterService formatterService, TaxSettings taxSettings, IDataSerializer dataSerializer)
        {
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _formatterService = formatterService;
            _taxSettings = taxSettings;
            _dataSerializer = dataSerializer;
        }

        public OrderModel Create(Order order)
        {
            var model = _modelMapper.Map<OrderModel>(order);
            if (order.BillingAddressSerialized != null)
            {
                var billingAddress = _dataSerializer.DeserializeAs<Address>(order.BillingAddressSerialized);
                model.BillingAddress = _modelMapper.Map<AddressInfoModel>(billingAddress);
                model.BillingAddress.StateProvinceName =
                    billingAddress.StateOrProvince?.Name ?? billingAddress.StateProvinceName;
                model.BillingAddress.CountryName = billingAddress.Country.Name;
            }


            if (order.ShippingAddressSerialized != null)
            {
                var shippingAddress = _dataSerializer.DeserializeAs<Address>(order.ShippingAddressSerialized);
                model.ShippingAddress = _modelMapper.Map<AddressInfoModel>(shippingAddress);
                model.ShippingAddress.StateProvinceName =
                    shippingAddress.StateOrProvince?.Name ?? shippingAddress.StateProvinceName;
                model.ShippingAddress.CountryName = shippingAddress.Country.Name;
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