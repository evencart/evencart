using System;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Infrastructure.Mvc.Models
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