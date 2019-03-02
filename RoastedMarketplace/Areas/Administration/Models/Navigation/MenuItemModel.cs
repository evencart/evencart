using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Navigation
{
    public class MenuItemModel : FoundationEntityModel, IRequiresValidations<MenuItemModel>
    {
        public int MenuId { get; set; }

        public int? ParentMenuItemId { get; set; }

        public string Name { get; set; }

        public int? SeoMetaId { get; set; }

        public string Url { get; set; }

        public int DisplayOrder { get; set; }

        public string CssClass { get; set; }

        public string SeoMetaTargetName { get; set; }

        public bool IsGroup { get; set; }

        public void SetupValidationRules(ModelValidator<MenuItemModel> v)
        {
            v.RuleFor(x => x.MenuId).GreaterThan(0);
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SeoMetaId).GreaterThan(0);
        }
    }
}