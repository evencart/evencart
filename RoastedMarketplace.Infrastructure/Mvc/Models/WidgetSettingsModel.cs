using System;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Infrastructure.Mvc.Models
{
    public abstract class WidgetSettingsModel : IRequiresValidations<WidgetSettingsModel>
    {
        public string Id { get; set; }

        public void SetupValidationRules(ModelValidator<WidgetSettingsModel> v)
        {
            v.RuleFor(x => x.Id).NotEmpty().Must(x => Guid.TryParse(x, out Guid _));
        }
    }
}