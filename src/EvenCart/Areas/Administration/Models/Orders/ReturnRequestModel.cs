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

using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Data.Entity.Purchases;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class ReturnRequestModel : GenesisEntityModel, IRequiresValidations<ReturnRequestModel>
    {
        public ReturnRequestStatus ReturnRequestStatus { get; set; }

        public ReturnOption ReturnOption { get; set; }

        public string ReturnOptionDisplay => ReturnOption.ToString();

        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public string AttributeText { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public int OrderedQuantity { get; set; }

        public string ReturnReason { get; set; }

        public string ReturnAction { get; set; }

        public string OrderNumber { get; set; }

        public int OrderId { get; set; }

        public string CustomerComments { get; set; }

        public string StaffComments { get; set; }

        public string Remarks { get; set; }

        public int ReturnOrderId { get; set; }

        public string ReturnOrderNumber { get; set; }

        public UserModel User { get; set; }

        public void SetupValidationRules(ModelValidator<ReturnRequestModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}