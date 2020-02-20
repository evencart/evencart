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