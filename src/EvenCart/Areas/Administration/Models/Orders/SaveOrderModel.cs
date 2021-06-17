﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class SaveOrderModel : GenesisEntityModel, IRequiresValidations<SaveOrderModel>
    {
        public string ShippingMethodName { get; set; }

        public string ShippingMethodDisplayName { get; set; }

        public string SelectedShippingOption { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDisplayName { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? ShippingMethodFee { get; set; }

        public decimal? PaymentMethodFee { get; set; }

        public decimal? Tax { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        public void SetupValidationRules(ModelValidator<SaveOrderModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}