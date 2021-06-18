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
using EvenCart.Areas.Administration.Models.Users;
using FluentValidation;
using Genesis;
using Genesis.Infrastructure;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Genesis.Modules.Localization;

namespace EvenCart.Areas.Administration.Models.Pages
{
    public class ContentPageModel : GenesisEntityModel, IRequiresValidations<ContentPageModel>
    {
        public string Name { get; set; }

        public string Content { get; set; }

        public bool Published { get; set; }

        public bool Private { get; set; }

        public string Password { get; set; }

        public string SystemName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime PublishedOn { get; set; }

        public int UserId { get; set; }

        public string Template { get; set; }

        public int ParentId { get; set; }

        public string ParentPath { get; set; }

        public IList<int> StoreIds { get; set; }

        #region Virtual Properties
        public SeoMetaModel SeoMeta { get; set; }

        public UserMiniModel User { get; set; }
        #endregion

        public void SetupValidationRules(ModelValidator<ContentPageModel> v)
        {
            var engine = D.Resolve<IGenesisEngine>();
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Template)
                .Must(x => x == null || x == "0" || engine.ActiveTheme.Templates.ContainsKey(x))
                .WithMessage(LocalizationHelper.Localize("{{PropertyName}} contains an unknown value"));
        }
    }
}