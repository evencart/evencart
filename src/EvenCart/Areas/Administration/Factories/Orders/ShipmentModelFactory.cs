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
using EvenCart.Areas.Administration.Factories.Warehouses;
using EvenCart.Areas.Administration.Models.Orders;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Services.Helpers;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.ModelFactories;
using Genesis.Modules.Data;

namespace EvenCart.Areas.Administration.Factories.Orders
{
    public class ShipmentModelFactory : IShipmentModelFactory
    {
        private readonly IModelMapper _modelMapper;
        private readonly IFormatterService _formatterService;
        private readonly IWarehouseModelFactory _warehouseModelFactory;
        public ShipmentModelFactory(IModelMapper modelMapper, IFormatterService formatterService, IWarehouseModelFactory warehouseModelFactory)
        {
            _modelMapper = modelMapper;
            _formatterService = formatterService;
            _warehouseModelFactory = warehouseModelFactory;
        }

        public ShipmentModel Create(Shipment shipment)
        {
            var shipmentModel = _modelMapper.Map<ShipmentModel>(shipment);
            shipmentModel.ShipmentItems = shipment.ShipmentItems?.Select(y =>
                                              {
                                                  var shipmentItemModel = _modelMapper.Map<ShipmentItemModel>(y);
                                                  shipmentItemModel.ProductName = y.OrderItem.Product.Name;
                                                  shipmentItemModel.OrderedQuantity = y.OrderItem.Quantity;
                                                  shipmentItemModel.Quantity = y.Quantity;
                                                  shipmentItemModel.AttributeText =
                                                      _formatterService.FormatProductAttributes(y.OrderItem
                                                          .AttributeJson);
                                                  return shipmentItemModel;
                                              })
                                              .ToList() ?? new List<ShipmentItemModel>();

            shipmentModel.ShipmentHistoryItems = shipment.ShipmentStatusHistories?.Select(x =>
                {
                    var historyModel = _modelMapper.Map<ShipmentHistoryModel>(x);
                    return historyModel;
                })
                .ToList();

            if (shipment.ShippingLabelUrl.IsNullEmptyOrWhiteSpace())
            {
                //does the shipping handler support purchase
                var shipmentHandler = PluginHelper.GetShipmentHandler(shipment.ShippingMethodName);
                shipmentModel.SupportsLabelPurchase = shipmentHandler?.SupportsLabelPurchase ?? false;
            }
            else
            {
                shipmentModel.SupportsLabelPurchase = false;
            }
            return shipmentModel;
        }

        public IList<ShipmentEditorModel> Create(IList<Shipment> shipments)
        {
            var models = new List<ShipmentEditorModel>();
            var grouped = shipments.GroupBy(x => x.Warehouse);
            foreach (var g in grouped)
            {
                var warehouse = g.Key;
                var wShipments = g.ToList();
                var model = new ShipmentEditorModel
                {
                    Warehouse = _warehouseModelFactory.CreateMini(warehouse),
                    Shipments = wShipments.Select(Create).ToList()
                };
                models.Add(model);
            }

            return models;
        }
    }
}