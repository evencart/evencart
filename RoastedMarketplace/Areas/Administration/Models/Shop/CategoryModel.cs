using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Shop
{
    public class CategoryModel : FoundationEntityModel, IRequiresValidations<CategoryModel>
    {
        public string FullCategoryPath { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public string ImageUrl { get; set; }

        public int ParentCategoryId { get; set; }

        public int MediaId { get; set; }

        public void SetupValidationRules(ModelValidator<CategoryModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}