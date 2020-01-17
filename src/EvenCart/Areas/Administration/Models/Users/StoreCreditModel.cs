using System;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class StoreCreditModel : FoundationEntityModel, IRequiresValidations<StoreCreditModel>
    {
        public int UserId { get; set; }

        public decimal Credit { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime AvailableOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public void SetupValidationRules(ModelValidator<StoreCreditModel> v)
        {
            v.RuleFor(x => x.UserId).GreaterThan(0);
            v.RuleFor(x => x.Description).NotEmpty();
        }
    }
}