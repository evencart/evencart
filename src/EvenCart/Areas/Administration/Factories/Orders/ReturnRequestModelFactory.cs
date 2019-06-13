using System.Linq;
using EvenCart.Areas.Administration.Factories.Users;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Services.Formatter;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public class ReturnRequestModelFactory : IReturnRequestModelFactory
    {
        private readonly IFormatterService _formatterService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IUserModelFactory _userModelFactory;
        public ReturnRequestModelFactory(IFormatterService formatterService, IMediaAccountant mediaAccountant, IUserModelFactory userModelFactory)
        {
            _formatterService = formatterService;
            _mediaAccountant = mediaAccountant;
            _userModelFactory = userModelFactory;
        }

        public ReturnRequestModel Create(ReturnRequest entity)
        {
            var model = new ReturnRequestModel()
            {
                Id = entity.Id,
                OrderedQuantity = entity.OrderItem.Quantity,
                Quantity = entity.Quantity,
                OrderNumber = entity.Order.OrderNumber,
                ProductName = entity.OrderItem.Product.Name,
                ProductId = entity.OrderItem.ProductId,
                AttributeText = _formatterService.FormatProductAttributes(entity.OrderItem.AttributeJson),
                ReturnRequestStatus = entity.ReturnRequestStatus,
                ImageUrl = _mediaAccountant.GetPictureUrl(entity.OrderItem.Product.MediaItems.FirstOrDefault()),
                Remarks = entity.Remarks,
                CustomerComments = entity.CustomerComments,
                ReturnAction = entity.ReturnAction,
                ReturnReason = entity.ReturnReason,
                StaffComments = entity.StaffComments,
                OrderId = entity.OrderId,
                User = _userModelFactory.Create(entity.User),
                ReturnOrderNumber = entity.ReturnOrder?.OrderNumber,
                ReturnOrderId = entity.ReturnOrder?.Id ?? 0,
                ReturnOption = entity.ReturnOption
            };
            return model;
        }
    }
}