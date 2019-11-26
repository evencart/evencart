using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class DownloadUploadModel : FoundationEntityModel, IRequiresValidations<DownloadUploadModel>
    {
        public IFormFile MediaFile { get; set; }

        public int ProductId { get; set; }

        public void SetupValidationRules(ModelValidator<DownloadUploadModel> v)
        {
            v.RuleFor(x => x.MediaFile).NotNull().Must(x => x.Length != 0);

            v.RuleFor(x => x.ProductId).GreaterThan(0).When(x => x.Id == 0);

            v.RuleFor(x => x.Id).GreaterThan(0).When(x => x.ProductId == 0);

        }
    }
}