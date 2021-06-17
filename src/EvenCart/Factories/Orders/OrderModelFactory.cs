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

using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Data.Entity.Settings;
using EvenCart.Factories.Shipments;
using EvenCart.Models.Addresses;
using EvenCart.Models.Orders;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.MediaServices;
using Genesis.Modules.Addresses;
using Genesis.Modules.Data;

namespace EvenCart.Factories.Orders
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly IFormatterService _formatterService;
        private readonly TaxSettings _taxSettings;
        private readonly IDataSerializer _dataSerializer;
        private readonly IShipmentModelFactory _shipmentModelFactory;
        public OrderModelFactory(IModelMapper modelMapper, IMediaAccountant mediaAccountant, IFormatterService formatterService, TaxSettings taxSettings, IDataSerializer dataSerializer, IShipmentModelFactory shipmentModelFactory)
        {
            _modelMapper = modelMapper;
            _mediaAccountant = mediaAccountant;
            _formatterService = formatterService;
            _taxSettings = taxSettings;
            _dataSerializer = dataSerializer;
            _shipmentModelFactory = shipmentModelFactory;
        }

        public OrderModel Create(Order order)
        {
            var model = _modelMapper.Map<OrderModel>(order);
            if (!order.SelectedShippingOption.IsNullEmptyOrWhiteSpace())
            {
                var selectedOptions =
                    _dataSerializer.DeserializeAs<IList<ShippingOption>>(order.SelectedShippingOption);
                model.SelectedShippingOption = string.Join(", ", selectedOptions.Select(x => x.Name));
            }
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
            //when at least one shipment has been added and shipped
            if (model.OrderItems != null && order.Shipments != null && order.Shipments.Any(x => (int)x.ShipmentStatus > (int)ShipmentStatus.Preparing))
            {
                model.Shipments = order.Shipments.Select(_shipmentModelFactory.Create).ToList();
                //remove order items which have already been shipped
                var shipmentItems = order.Shipments.SelectMany(x => x.ShipmentItems).ToList();
                foreach (var orderItemModel in model.OrderItems)
                {
                    var shippedItemCount = shipmentItems.Where(x => x.OrderItemId == orderItemModel.Id)
                        .Sum(x => x.Quantity);
                    if (orderItemModel.Quantity > shippedItemCount)
                    {
                        //fewer items were shipped
                        orderItemModel.Quantity = orderItemModel.Quantity - shippedItemCount;
                    }
                    else
                    {
                        //we'll remove this item when loop is done
                        orderItemModel.Id = 0;
                    }
                }
                //remove all the order items with id 0
                while (model.OrderItems.Any(x => x.Id == 0))
                    model.OrderItems.Remove(model.OrderItems.First(x => x.Id == 0));
            }

            if (model.OrderItems != null)
            {
                model.Taxes = model.OrderItems.GroupBy(x => new {x.TaxName, x.TaxPercent}).Select(x =>
                    new OrderTaxModel()
                    {
                        TaxName = x.Key.TaxName,
                        TaxPercent = x.Key.TaxPercent,
                        Amount = x.Sum(y => y.Tax)
                    }).ToList();
            }
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
            orderItemModel.SeName = orderItem.Product?.SeoMeta?.Slug;
            orderItemModel.TaxName = orderItem.TaxName;
            orderItemModel.IsDownloadable = orderItem.IsDownloadable;
            return orderItemModel;
        }
    }
}