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

using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Emails
{
    public class EmailTemplateModel : FoundationEntityModel, IRequiresValidations<EmailTemplateModel>
    {
        public string TemplateName { get; set; }

        public string TemplateSystemName { get; set; }

        public string Template { get; set; }

        public bool IsMaster { get; set; }

        public int ParentEmailTemplateId { get; set; }

        public int EmailAccountId { get; set; }

        public string Subject { get; set; }

        public string AdministrationEmail { get; set; }

        public EmailTemplateModel ParentEmailTemplate { get; set; }

        public EmailAccountModel EmailAccount { get; set; }

        public void SetupValidationRules(ModelValidator<EmailTemplateModel> v)
        {
            v.RuleFor(x => x.TemplateName).NotEmpty();
            v.RuleFor(x => x.TemplateSystemName).NotEmpty().When(x => x.Id == 0);
            v.RuleFor(x => x.EmailAccountId).GreaterThan(0);
        }
    }
}