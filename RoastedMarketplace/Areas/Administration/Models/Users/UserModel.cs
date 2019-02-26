using System;
using System.Collections.Generic;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Users
{
    public class UserModel : FoundationEntityModel, IRequiresValidations<UserModel>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CompanyName { get; set; }

        public string MobileNumber { get; set; }

        public string Guid { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string Remarks { get; set; }

        public string LastLoginIpAddress { get; set; }

        public int ReferrerId { get; set; }

        public IList<RoleModel> Roles { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public void SetupValidationRules(ModelValidator<UserModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}