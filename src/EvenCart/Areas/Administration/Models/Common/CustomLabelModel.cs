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

using FluentValidation;
using Genesis.Infrastructure.Mvc.Models;
using Genesis.Infrastructure.Mvc.Validator;
using Genesis.Modules.Meta;

namespace EvenCart.Areas.Administration.Models.Common
{
    /// <summary>
    /// Represents a custom label
    /// </summary>
    public class CustomLabelModel : GenesisEntityModel, IRequiresValidations<CustomLabelModel>
    {
        /// <summary>
        /// The text of the label
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The <see cref="CustomLabelType">label type</see>
        /// </summary>
        public string LabelType { get; set; }

        /// <summary>
        /// The display order of the label
        /// </summary>
        public int DisplayOrder { get; set; }

        public void SetupValidationRules(ModelValidator<CustomLabelModel> v)
        {
            v.RuleFor(x => x.Text).NotEmpty();
            v.RuleFor(x => x.LabelType).NotEmpty();
        }
    }
}