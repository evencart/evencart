using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Products
{
    public class UploadFileModel : FoundationModel, IRequiresValidations<UploadFileModel>
    {
        public IFormFile MediaFile { get; set; }

        public int ProductId { get; set; }

        public void SetupValidationRules(ModelValidator<UploadFileModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull().Must(x => x.Length != 0);

            v.RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}