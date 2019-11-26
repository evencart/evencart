using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class DownloadModel : FoundationEntityModel, IRequiresValidations<DownloadModel>
    {
        public string Guid { get; set; }

        public int ProductId { get; set; }

        public int ProductVariantId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string FileLocation { get; set; }

        public bool IsFileLocationUrl { get; set; }

        public string DownloadUrl { get; set; }

        public bool RequirePurchase { get; set; }

        public bool RequireLogin { get; set; }

        public int MaximumDownloads { get; set; }

        public int DisplayOrder { get; set; }

        public DownloadActivationType DownloadActivationType { get; set; }

        public bool Published { get; set; }

        public void SetupValidationRules(ModelValidator<DownloadModel> v)
        {
            v.RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}