using System;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Models.Reviews
{
    public class ReviewModel : FoundationEntityModel, IRequiresValidations<ReviewModel>
    {
        public int Rating { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool VerifiedPurchase { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool Private { get; set; }

        public string DisplayName { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public void SetupValidationRules(ModelValidator<ReviewModel> v)
        {
            v.RuleFor(x => x.Rating).GreaterThan(0).LessThan(6);
            v.RuleFor(x => x.ProductId).GreaterThan(0);
            v.RuleFor(x => x.OrderId).GreaterThan(0);
        }
    }
}