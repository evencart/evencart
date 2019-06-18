using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Plugins
{
    /// <summary>
    /// Represents a plugin search object
    /// </summary>
    public class PluginsSearchModel : AdminSearchModel, IRequiresValidations<PluginsSearchModel>
    {
        public void SetupValidationRules(ModelValidator<PluginsSearchModel> v)
        {
            v.RuleFor(x => x.Current).GreaterThan(0);
            v.RuleFor(x => x.RowCount).LessThanOrEqualTo(30).GreaterThanOrEqualTo(0);
        }
    }
}