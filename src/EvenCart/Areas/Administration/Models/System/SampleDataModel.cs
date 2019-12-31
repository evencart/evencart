using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Models.System
{
    public class SampleDataModel : FoundationModel, IRequiresValidations<SampleDataModel>
    {
        public IFormFile MediaFile { get; set; }

        public void SetupValidationRules(ModelValidator<SampleDataModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull();
        }
    }
}
