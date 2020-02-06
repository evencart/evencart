using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Navigation
{
    public class MenuItemModel : FoundationEntityModel, IRequiresValidations<MenuItemModel>
    {
        public int MenuId { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public int? SeoMetaId { get; set; }

        public string Url { get; set; }

        public int DisplayOrder { get; set; }

        public string CssClass { get; set; }

        public string SeoMetaTargetName { get; set; }

        public bool IsGroup { get; set; }

        public bool OpenInNewWindow { get; set; }

        public string Description { get; set; }

        public string ExtraData { get; set; }

        public void SetupValidationRules(ModelValidator<MenuItemModel> v)
        {
            v.RuleFor(x => x.MenuId).GreaterThan(0);
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SeoMetaId).GreaterThan(0);
        }
    }
}