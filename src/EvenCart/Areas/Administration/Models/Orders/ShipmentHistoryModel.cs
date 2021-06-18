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

using System;
using EvenCart.Data.Entity.Purchases;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ShipmentHistoryModel : GenesisEntityModel, IRequiresValidations<ShipmentHistoryModel>
    {
        public int ShipmentId { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public string ShipmentStatusDisplay => ShipmentStatus.ToString();

        public DateTime CreatedOn { get; set; }

        public void SetupValidationRules(ModelValidator<ShipmentHistoryModel> v)
        {
            v.RuleFor(x => x.ShipmentStatus).NotEmpty().When(x => x.Id < 1);
            v.RuleFor(x => x.CreatedOn).NotEmpty().When(x => x.Id > 0);
            v.RuleFor(x => x.ShipmentId).GreaterThan(0);
        }
    }
}