using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Taxes
{
    public class TaxRateModel : FoundationEntityModel, IRequiresValidations<TaxRateModel>
    {
        public int TaxId { get; set; }

        public int CountryId { get; set; }

        public int? StateOrProvinceId { get; set; }

        public string ZipOrPostalCode { get; set; }

        public string CountryName { get; set; }

        public string StateOrProvinceName { get; set; }

        public decimal Rate { get; set; } = 0;

        public void SetupValidationRules(ModelValidator<TaxRateModel> v)
        {
            v.RuleFor(x => x.TaxId).GreaterThan(0);
        }
    }
}