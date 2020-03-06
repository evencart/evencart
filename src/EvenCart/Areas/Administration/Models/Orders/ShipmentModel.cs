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
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ShipmentModel : FoundationEntityModel, IRequiresValidations<ShipmentModel>
    {
        public int OrderId { get; set; }

        public string TrackingNumber { get; set; }

        public string Remarks { get; set; }

        public string ShippingMethodName { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public string ShipmentStatusDisplay => ShipmentStatus.ToString();

        public int ShipmentStatusId => (int) ShipmentStatus;

        public IList<ShipmentItemModel> ShipmentItems { get; set; }

        public IList<ShipmentHistoryModel> ShipmentHistoryItems { get; set; }

        public int WarehouseId { get; set; }

        public string ShippingLabelUrl { get; set; }

        public string TrackingUrl { get; set; }

        public bool SupportsLabelPurchase { get; set; }

        public void SetupValidationRules(ModelValidator<ShipmentModel> v)
        {
            v.RuleFor(x => x.OrderId).GreaterThan(0);
            v.RuleFor(x => x.WarehouseId).GreaterThan(0);
        }
    }
}