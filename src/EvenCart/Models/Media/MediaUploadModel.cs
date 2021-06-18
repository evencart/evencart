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
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Media
{
    /// <summary>
    /// Represents an uploaded media file
    /// </summary>
    public class MediaUploadModel : GenesisModel, IRequiresValidations<MediaUploadModel>
    {
        /// <summary>
        /// The file object
        /// </summary>
        public IFormFile MediaFile { get; set; }

        /// <summary>
        /// The name of the entity with which this media is to be linked
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// The id of the entity
        /// </summary>
        public int EntityId { get; set; }

        public void SetupValidationRules(ModelValidator<MediaUploadModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull().Must(x => x.Length != 0);
        }
    }
}