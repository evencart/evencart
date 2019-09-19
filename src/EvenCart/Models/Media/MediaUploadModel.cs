using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Media
{
    /// <summary>
    /// Represents an uploaded media file
    /// </summary>
    public class MediaUploadModel : FoundationModel, IRequiresValidations<MediaUploadModel>
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