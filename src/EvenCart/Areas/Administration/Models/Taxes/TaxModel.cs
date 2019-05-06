using System.Collections.Generic;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Taxes
{
    public class TaxModel : FoundationEntityModel, IRequiresValidations<TaxModel>
    {
        public string Name { get; set; }

        #region Virtual Properties

        public virtual IList<TaxRateModel> TaxRates { get; set; }

        #endregion

        public void SetupValidationRules(ModelValidator<TaxModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
        }
    }
}