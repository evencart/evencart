using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Users
{
    public class UserSearchModel : SearchModel, IRequiresValidations<UserSearchModel>
    {
        public int[] RoleIds { get; set; }

        public void SetupValidationRules(ModelValidator<UserSearchModel> v)
        {
            v.RuleFor(x => x.RowCount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            v.RuleFor(x => x.Current).GreaterThanOrEqualTo(1);
        }
    }
}