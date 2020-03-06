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
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Models.Plugins
{
    public class UploadPackageModel : FoundationModel, IRequiresValidations<UploadPackageModel>
    {
        public IFormFile MediaFile { get; set; }

        public void SetupValidationRules(ModelValidator<UploadPackageModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull();
        }
    }
}