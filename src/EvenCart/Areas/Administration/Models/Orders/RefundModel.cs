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

using EvenCart.Data.Entity.Purchases;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class RefundModel : GenesisModel, IRequiresValidations<RefundModel>
    {
        public bool RefundOffline { get; set; }

        public bool IsPartialRefund { get; set; }

        public decimal Amount { get; set; }

        public int OrderId { get; set; }

        public RefundType RefundType { get; set; }

        public void SetupValidationRules(ModelValidator<RefundModel> v)
        {
            v.RuleFor(x => x.Amount).GreaterThan(0).When(x => x.IsPartialRefund);
            v.RuleFor(x => x.OrderId).GreaterThan(0);
        }
    }
}