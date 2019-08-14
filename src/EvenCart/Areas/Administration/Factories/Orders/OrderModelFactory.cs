using System.Linq;
using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Addresses;
using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Services.Formatter;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Services.Helpers;
using EvenCart.Services.MediaServices;
using EvenCart.Services.Serializers;
using Microsoft.CodeAnalysis.Emit;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IFormatterService _formatterService;
        private readonly IDataSerializer _dataSerializer;
        private readonly IMediaAccountant _mediaAccountant;
        public OrderModelFactory(IModelMapper modelMapper, IFormatterService formatterService, IDataSerializer dataSerializer, IMediaAccountant mediaAccountant)
        {
            _modelMapper = modelMapper;
            _formatterService = formatterService;
            _dataSerializer = dataSerializer;
            _mediaAccountant = mediaAccountant;
        }

        public OrderModel Create(Order entity)
        {
            var model = _modelMapper.Map<OrderModel>(entity);
            model.User = _modelMapper.Map<UserModel>(entity.User);
            if (entity.BillingAddressSerialized != null)
            {
                var billingAddress = _dataSerializer.DeserializeAs<Address>(entity.BillingAddressSerialized);
                model.BillingAddress = _modelMapper.Map<AddressModel>(billingAddress);
                model.BillingAddress.CountryName = billingAddress.Country.Name;
                model.BillingAddress.StateProvinceName = billingAddress.StateOrProvince?.Name ?? billingAddress.StateProvinceName;
            }

            if (entity.ShippingAddressSerialized != null)
            {
                var shippingAddress = _dataSerializer.DeserializeAs<Address>(entity.ShippingAddressSerialized);
                model.ShippingAddress = _modelMapper.Map<AddressModel>(shippingAddress);
                model.ShippingAddress.CountryName = shippingAddress.Country.Name;
                model.ShippingAddress.StateProvinceName = shippingAddress.StateOrProvince?.Name ?? shippingAddress.StateProvinceName;
            }
            model.OrderItems = entity.OrderItems?.Select(Create).ToList();
            return model;
        }

        public OrderItemModel Create(OrderItem entity)
        {
            var orderItemModel = _modelMapper.Map<OrderItemModel>(entity);
            orderItemModel.ProductName = entity.Product?.Name;
            orderItemModel.AttributeText = _formatterService.FormatProductAttributes(entity.AttributeJson);
            orderItemModel.TotalPrice = orderItemModel.Price * orderItemModel.Quantity;
            if (entity.Product?.MediaItems != null && entity.Product.MediaItems.Any())
            {
                orderItemModel.ImageUrl = _mediaAccountant.GetPictureUrl(entity.Product.MediaItems.First(), returnDefaultIfNotFound: true);
            }
            return orderItemModel;
        }

        public PaymentTransactionModel Create(PaymentTransaction entity)
        {
            var model = _modelMapper.Map<PaymentTransactionModel>(entity);
            var paymentHandler = PluginHelper.GetPaymentHandler(entity.PaymentMethodName);
            if (paymentHandler != null)
            {
                model.PaymentMethodDisplay = paymentHandler.PluginInfo.Name;
            }
            return model;
        }
    }
}