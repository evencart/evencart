using FluentValidation;
using RoastedMarketplace.Data.Entity.Addresses;
using RoastedMarketplace.Infrastructure.Mvc.Models;
using RoastedMarketplace.Infrastructure.Mvc.Validator;

namespace RoastedMarketplace.Models.Addresses
{
    public class AddressInfoModel : FoundationEntityModel, IRequiresValidations<AddressInfoModel>
    {
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Landmark { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public string ZipPostalCode { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Email { get; set; }

        public AddressType AddressType { get; set; } = AddressType.Home;

        public void SetupValidationRules(ModelValidator<AddressInfoModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Address1).NotEmpty();
            v.RuleFor(x => x.City).NotEmpty();
            v.RuleFor(x => x.ZipPostalCode).NotEmpty();
            v.RuleFor(x => x.CountryId).NotEmpty().GreaterThan(0);
            v.RuleFor(x => x.StateProvinceName).NotEmpty()
                .When(x => !x.StateProvinceId.HasValue || x.StateProvinceId <= 0);
        }
    }
}