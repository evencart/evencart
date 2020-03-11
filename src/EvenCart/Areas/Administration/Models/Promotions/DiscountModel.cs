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
using System.Collections.Generic;
using EvenCart.Data.Entity.Promotions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Promotions
{
    public class DiscountModel : FoundationEntityModel, IRequiresValidations<DiscountModel>
    {
        public string Name { get; set; }

        public bool HasCouponCode { get; set; }

        public string CouponCode { get; set; }

        public int NumberOfTimesPerUser { get; set; }

        public int TotalNumberOfTimes { get; set; }

        public decimal MaximumDiscountAmount { get; set; }

        public CalculationType CalculationType { get; set; }

        public string CalculationTypeDisplay => CalculationType.ToString();

        public decimal DiscountValue { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool ExcludeAlreadyDiscountedProducts { get; set; }

        public bool Expired { get; set; }

        public bool Enabled { get; set; }

        public RestrictionType RestrictionType { get; set; }

        public decimal MinimumOrderSubTotal { get; set; }

        public string RestrictionTypeDisplay => RestrictionType.ToString();

        public IList<RestrictionValueModel> RestrictionValues { get; set; }

        public void SetupValidationRules(ModelValidator<DiscountModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}