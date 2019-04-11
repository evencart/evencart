using System.Linq;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Formatter;
using EvenCart.Infrastructure.Mvc.ModelFactories;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IFormatterService _formatterService;
        public OrderModelFactory(IModelMapper modelMapper, IFormatterService formatterService)
        {
            _modelMapper = modelMapper;
            _formatterService = formatterService;
        }

        public OrderModel Create(Order entity)
        {
            var model = _modelMapper.Map<OrderModel>(entity);
            model.User = _modelMapper.Map<UserModel>(entity.User);
            model.BillingAddress = _modelMapper.Map<AddressModel>(entity.BillingAddress);
            model.ShippingAddress = _modelMapper.Map<AddressModel>(entity.ShippingAddress);
            model.OrderItems = entity.OrderItems?.Select(Create).ToList();
            return model;
        }

        public OrderItemModel Create(OrderItem entity)
        {
            var orderItemModel = _modelMapper.Map<OrderItemModel>(entity);
            orderItemModel.ProductName = entity.Product.Name;
            orderItemModel.AttributeText = _formatterService.FormatProductAttributes(entity.AttributeJson);
            orderItemModel.TotalPrice = orderItemModel.Price * orderItemModel.Quantity;
            return orderItemModel;
        }
    }
}