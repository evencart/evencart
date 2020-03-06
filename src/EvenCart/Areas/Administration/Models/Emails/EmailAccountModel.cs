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

using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Emails
{
    public class EmailAccountModel : FoundationEntityModel, IRequiresValidations<EmailAccountModel>
    {
        public string Email { get; set; }

        public string FromName { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool UseSsl { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsDefault { get; set; }

        public string TestEmail { get; set; }

        public void SetupValidationRules(ModelValidator<EmailAccountModel> v)
        {
            v.RuleFor(x => x.Email).EmailAddress();
            v.RuleFor(x => x.TestEmail).Must(s => s.IsNullEmptyOrWhiteSpace() || s.IsValidEmail());
            v.RuleFor(x => x.Password).NotEmpty().When(x => x.Id == 0);
        }
    }
}