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
using EvenCart.Models.Products;
using FluentValidation;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Models.Reviews
{
    public class ReviewModel : GenesisEntityModel, IRequiresValidations<ReviewModel>
    {
        /// <summary>
        /// The rating value between 1 and 5
        /// </summary>
        public int Rating { get; set; }

        public decimal RatingPercent => (decimal) Rating / 5 * 100;
        /// <summary>
        /// The title of the review
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The description of the review
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies if the review was posted by the user who bought from the store. Ignored for POST requests.
        /// </summary>
        public bool VerifiedPurchase { get; set; }
        /// <summary>
        /// The creation date of review. Ignored for POST requests.
        /// </summary>
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The creation date of review in user's timezone
        /// </summary>
        public DateTime CreatedOnLocal => CreatedOn.ToUserDateTime();
        /// <summary>
        /// Specifies if user information should be kept private or public
        /// </summary>
        public bool Private { get; set; }
        /// <summary>
        /// The display name of user for review. Ignored for POST requests.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The id of product for which review is being posted
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The id of order for which review is being posted
        /// </summary>
        public int OrderId { get; set; }

        public virtual ProductModel Product { get; set; }
        public void SetupValidationRules(ModelValidator<ReviewModel> v)
        {
            v.RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
            v.RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}