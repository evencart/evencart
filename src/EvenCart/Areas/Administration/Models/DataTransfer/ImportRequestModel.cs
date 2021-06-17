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
using Genesis.Infrastructure.Mvc.Validator;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Models.DataTransfer
{
    /// <summary>
    /// Represents the import request
    /// </summary>
    public class ImportRequestModel : IRequiresValidations<ImportRequestModel>
    {
        /// <summary>
        /// The name of entity to be imported
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// The file which will be imported
        /// </summary>
        public IFormFile ImportFile { get; set; }

        /// <summary>
        /// The input file format. Can be either json or excel.
        /// </summary>
        public string Input { get; set; }

        public void SetupValidationRules(ModelValidator<ImportRequestModel> v)
        {
            v.RuleFor(x => x.EntityName).NotEmpty();
            v.RuleFor(x => x.ImportFile).NotNull();
            v.RuleFor(x => x.Input).Must(x => x == "json" || x == "excel");
        }
    }
}