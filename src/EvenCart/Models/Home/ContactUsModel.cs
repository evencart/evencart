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

using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Models.Home
{
    public class ContactUsModel : FoundationModel, IRequiresValidations<ContactUsModel>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public void SetupValidationRules(ModelValidator<ContactUsModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Email).EmailAddress().NotEmpty();
            v.RuleFor(x => x.Subject).Must((rootObject, x, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", 10);
                return x.Trim().Length > 10;
            }).WithMessage(LocalizationHelper.Localize("{{PropertyName}} should be at least {{MinLength}} characters long",
                ApplicationEngine.CurrentLanguageCultureCode));

            v.RuleFor(x => x.Description).Must((rootObject, x, context) =>
            {
                context.MessageFormatter.AppendArgument("MinLength", 30);
                return x.Trim().Length > 30;
            }).WithMessage(LocalizationHelper.Localize("{{PropertyName}} should be at least {{MinLength}} characters long",
                ApplicationEngine.CurrentLanguageCultureCode));
        }
    }
}