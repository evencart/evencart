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

using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Reports
{
    public class StockReportSearchModel : AdminSearchModel, IRequiresValidations<StockReportSearchModel>
    {
        public string ProductSearch { get; set; }

        public bool? Published { get; set; }

        public int WarehouseId { get; set; }

        public void SetupValidationRules(ModelValidator<StockReportSearchModel> v)
        {
            v.RuleFor(x => x.WarehouseId).GreaterThan(0);
        }
    }
}