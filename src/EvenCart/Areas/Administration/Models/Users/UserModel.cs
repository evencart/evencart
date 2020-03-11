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
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Users
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

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool IsTaxExempt { get; set; }

        public string Remarks { get; set; }

        public string LastLoginIpAddress { get; set; }

        public int ReferrerId { get; set; }

        public bool NewslettersEnabled { get; set; }

        public IList<RoleModel> Roles { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int Points { get; set; }

        public int? ProfilePictureId { get; set; }

        public bool RequirePasswordChange { get; set; }

        public bool IsAffiliate { get; set; }

        public bool AffiliateActive { get; set; }

        public DateTime? AffiliateFirstActivationDate { get; set; }

        public void SetupValidationRules(ModelValidator<UserModel> v)
        {
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.Password).NotEmpty().When(x => x.Id == 0);
            v.RuleFor(x => x.Password).Equal(x => x.ConfirmPassword, StringComparer.InvariantCulture);
        }
    }
}