using System.Collections.Generic;
using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Taxes
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