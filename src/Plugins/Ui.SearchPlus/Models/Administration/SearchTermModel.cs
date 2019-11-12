using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace Ui.SearchPlus.Models.Administration
{
    public class SearchTermModel : FoundationEntityModel, IRequiresValidations<SearchTermModel>
    {
        public string Term { get; set; }

       public string TermCategory { get; set; }

       public int Score { get; set; }

        public void SetupValidationRules(ModelValidator<SearchTermModel> v)
        {
            v.RuleFor(x => x.Term).NotEmpty();
        }
    }
}