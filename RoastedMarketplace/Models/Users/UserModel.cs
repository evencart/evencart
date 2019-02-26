using System;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Models.Users
{
    public class UserModel : FoundationEntityModel, IRequiresValidations<UserModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string MobileNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Referrer { get; set; }

        public bool NewslettersEnabled { get; set; }

        public void SetupValidationRules(ModelValidator<UserModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}