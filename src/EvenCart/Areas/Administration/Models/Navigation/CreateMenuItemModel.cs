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

using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Navigation
{
    public class CreateMenuItemModel : FoundationModel, IRequiresValidations<CreateMenuItemModel>
    {
        public IList<int> CategoryIds { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public IList<int> ContentPageIds { get; set; }

        public int MenuId { get; set; }

        public int ParentMenuItemId { get; set; }

        public bool IsGroup { get; set; }

        public void SetupValidationRules(ModelValidator<CreateMenuItemModel> v)
        {
            v.RuleFor(x => x.MenuId).GreaterThan(0);
        }
    }
}