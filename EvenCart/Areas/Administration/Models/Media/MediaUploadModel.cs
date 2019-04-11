using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Models.Media
{
    public class MediaUploadModel : FoundationModel, IRequiresValidations<MediaUploadModel>
    {
        public IFormFile MediaFile { get; set; }

        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public void SetupValidationRules(ModelValidator<MediaUploadModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull().Must(x => x.Length != 0);
            v.RuleFor(x => x.EntityName.ToLower() == "product");
        }
    }
}