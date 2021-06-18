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
using FluentValidation;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class RoleModel : GenesisEntityModel, IRequiresValidations<RoleModel>
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemRole { get; set; }

        public List<string> Capabilities { get; set; }

        public void SetupValidationRules(ModelValidator<RoleModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SystemName).Custom((s, context) =>
            {
                var instanceToValidate = (RoleModel) context.InstanceToValidate;
                if (instanceToValidate.Id == 0 && s.IsNullEmptyOrWhiteSpace())
                {
                    context.AddFailure(nameof(SystemName), "System Name can't be empty");
                }
            });
        }
    }
}