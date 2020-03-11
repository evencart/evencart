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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Reviews
{
    public class ReviewModel : FoundationEntityModel, IRequiresValidations<ReviewModel>
    {
        public int Rating { get; set; }

        public decimal RatingPercent => (decimal)Rating / 5 * 100;

        public string Title { get; set; }

        public string Description { get; set; }

        public bool VerifiedPurchase { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool Private { get; set; }

        public string DisplayName { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public bool Published { get; set; }

        public void SetupValidationRules(ModelValidator<ReviewModel> v)
        {
            v.RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
        }
    }
}