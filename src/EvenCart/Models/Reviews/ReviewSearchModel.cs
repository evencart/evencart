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

using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Reviews
{
    public class ReviewSearchModel : PublicSearchModel, IRequiresValidations<ReviewSearchModel>
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// true if only reviews with verified purchase should be returned. false otherwise.
        /// </summary>
        public bool VerifiedPurchase { get; set; }
        /// <summary>
        /// Set to a value to search reviews of that rating value
        /// </summary>
        public int? Rating { get; set; }

        public void SetupValidationRules(ModelValidator<ReviewSearchModel> v)
        {
            v.RuleFor(x => x.Page).GreaterThan(0);
            v.RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}