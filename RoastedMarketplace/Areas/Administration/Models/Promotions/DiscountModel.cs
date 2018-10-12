using System;
using FluentValidation;
using RoastedMarketplace.Data.Entity.Promotions;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Promotions
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

        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; }

        public bool ExcludeAlreadyDiscountedProducts { get; set; }

        public bool Expired { get; set; }

        public bool Enabled { get; set; }

        public RestrictionType RestrictionType { get; set; }

        public string RestrictionValues { get; set; }

        public void SetupValidationRules(ModelValidator<DiscountModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}