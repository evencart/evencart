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
using EvenCart.Data.Entity.Settings;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Models.Shipments;
using EvenCart.Services.Formatter;

namespace EvenCart.Factories.Shipments
{
    public class ShipmentModelFactory : IShipmentModelFactory
    {
        private readonly IFormatterService _formatterService;
        private readonly IMediaAccountant _mediaAccountant;
        private readonly TaxSettings _taxSettings;
        public ShipmentModelFactory(IFormatterService formatterService, IMediaAccountant mediaAccountant, TaxSettings taxSettings)
        {
            _formatterService = formatterService;
            _mediaAccountant = mediaAccountant;
            _taxSettings = taxSettings;
        }

        public ShipmentModel Create(Shipment entity)
        {
            var model = new ShipmentModel()
            {
                ShipmentStatus = entity.ShipmentStatus,
                ShippingMethodName = entity.ShippingMethodName,
                TrackingNumber = entity.TrackingNumber,
                ShipmentItems = entity.ShipmentItems.Select(Create).ToList(),
                Id = entity.Id,
                TrackingUrl = entity.TrackingUrl
            };
            return model;
        }

        public ShipmentItemModel Create(ShipmentItem entity)
        {
            var shipmentItemModel = new ShipmentItemModel()
            {
                ProductName = entity.OrderItem.Product.Name,
                OrderedQuantity = entity.OrderItem.Quantity,
                ShippedQuantity = entity.Quantity,
                OrderItemId = entity.OrderItemId,
                AttributeText = _formatterService.FormatProductAttributes(entity.OrderItem.AttributeJson),
                ImageUrl = _mediaAccountant.GetPictureUrl(entity.OrderItem.Product.MediaItems?.FirstOrDefault(), returnDefaultIfNotFound: true),
                Price = _taxSettings.DisplayProductPricesWithoutTax
                    ? entity.OrderItem.Price
                    : entity.OrderItem.Price + entity.OrderItem.Tax / entity.OrderItem.Quantity,
                SeName = entity.OrderItem.Product.SeoMeta.Slug
            };
            return shipmentItemModel;
        }
    }
}