using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Shop
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