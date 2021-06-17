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
using System.Linq;
using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class AvailableAttributeModel : GenesisEntityModel, IRequiresValidations<AvailableAttributeModel>
    {
        /// <summary>
        /// The name of the attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the attribute
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of <see cref="AvailableAttributeValueModel">attributeValue</see> objects
        /// </summary>
        public IList<AvailableAttributeValueModel> AttributeValues { get; set; }

        public void SetupValidationRules(ModelValidator<AvailableAttributeModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.AttributeValues).Must(x => x.Any());
        }
    }
}