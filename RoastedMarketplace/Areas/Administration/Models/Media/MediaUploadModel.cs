using FluentValidation;
using Microsoft.AspNetCore.Http;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Media
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