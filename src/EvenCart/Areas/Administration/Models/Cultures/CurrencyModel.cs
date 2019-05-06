using EvenCart.Data.Entity.Cultures;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Cultures
{
    public class CurrencyModel : FoundationEntityModel, IRequiresValidations<CurrencyModel>
    {
        public string Name { get; set; }

        public string IsoCode { get; set; }

        public decimal ExchangeRate { get; set; }

        public string CultureCode { get; set; }

        public string CustomFormat { get; set; }

        public string Flag { get; set; }

        public bool Published { get; set; }

        public Rounding RoundingType { get; set; }

        public int NumberOfDecimalPlaces { get; set; }

        public string FlagUrl { get; set; }

        public void SetupValidationRules(ModelValidator<CurrencyModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.IsoCode).NotEmpty();
        }
    }
}