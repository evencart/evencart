#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Models.Orders;
using EvenCart.Services.Formatter;

namespace EvenCart.Factories.Orders
{
    public class ReturnRequestModelFactory : IReturnRequestModelFactory
    {
        private readonly IFormatterService _formatterService;
        private readonly IMediaAccountant _mediaAccountant;
        public ReturnRequestModelFactory(IFormatterService formatterService, IMediaAccountant mediaAccountant)
        {
            _formatterService = formatterService;
            _mediaAccountant = mediaAccountant;
        }

        public ReturnRequestInfoModel Create(ReturnRequest entity)
        {
            var model = new ReturnRequestInfoModel()
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                OrderNumber = entity.Order.OrderNumber,
                ProductName = entity.OrderItem.Product.Name,
                SeName = entity.OrderItem.Product.SeoMeta.Slug,
                ProductId = entity.OrderItem.ProductId,
                AttributeText = _formatterService.FormatProductAttributes(entity.OrderItem.AttributeJson),
                ReturnRequestStatus = entity.ReturnRequestStatus,
                ImageUrl = _mediaAccountant.GetPictureUrl(entity.OrderItem.Product.MediaItems.FirstOrDefault()),
                Remarks = entity.Remarks,
                CustomerComments = entity.CustomerComments,
                ReturnAction = entity.ReturnAction,
                ReturnReason = entity.ReturnReason,
                StaffComments = entity.StaffComments,
                OrderGuid = entity.Order.Guid,
                ReturnOrderGuid = entity.ReturnOrder?.Guid,
                ReturnOrderNumber = entity.ReturnOrder?.OrderNumber,
                ReturnOption = entity.ReturnOption
                
            };
            return model;
        }
    }
}