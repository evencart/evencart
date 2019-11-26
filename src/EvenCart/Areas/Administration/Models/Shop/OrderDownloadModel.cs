using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class OrderDownloadModel : FoundationModel, IRequiresValidations<OrderDownloadModel>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int DownloadCount { get; set; }

        public bool Active { get; set; }

        public string DownloadUrl { get; set; }

        public int ItemDownloadId { get; set; }

        public int DownloadId { get; set; }

        public void SetupValidationRules(ModelValidator<OrderDownloadModel> v)
        {
            v.RuleFor(x => x.DownloadId).GreaterThan(0);
        }
    }
}