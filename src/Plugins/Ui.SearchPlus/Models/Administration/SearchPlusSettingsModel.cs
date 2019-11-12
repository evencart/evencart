using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace Ui.SearchPlus.Models.Administration
{
    public class SearchPlusSettingsModel : FoundationModel, IRequiresValidations<SearchPlusSettingsModel>
    {
        public int NumberOfAutoCompleteResults { get; set; }

        public bool ShowTermCategory { get; set; }

        public void SetupValidationRules(ModelValidator<SearchPlusSettingsModel> v)
        {
            v.RuleFor(x => x.NumberOfAutoCompleteResults).GreaterThan(0);
        }
    }
}