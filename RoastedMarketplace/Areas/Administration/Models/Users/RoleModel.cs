using System.Collections.Generic;
using FluentValidation;
using RoastedMarketplace.Data.Extensions;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Users
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