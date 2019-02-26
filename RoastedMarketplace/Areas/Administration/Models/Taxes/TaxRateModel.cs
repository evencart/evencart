using FluentValidation;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Areas.Administration.Models.Taxes
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