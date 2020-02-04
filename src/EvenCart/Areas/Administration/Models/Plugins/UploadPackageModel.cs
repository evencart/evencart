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