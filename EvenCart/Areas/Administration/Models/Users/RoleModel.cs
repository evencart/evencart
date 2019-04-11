using System.Collections.Generic;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Users
{
    public class RoleModel : FoundationEntityModel, IRequiresValidations<RoleModel>
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemRole { get; set; }

        public List<string> Capabilities { get; set; }

        public void SetupValidationRules(ModelValidator<RoleModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.SystemName).Custom((s, context) =>
            {
                var instanceToValidate = (RoleModel) context.InstanceToValidate;
                if (instanceToValidate.Id == 0 && s.IsNullEmptyOrWhiteSpace())
                {
                    context.AddFailure(nameof(SystemName), "System Name can't be empty");
                }
            });
        }
    }
}