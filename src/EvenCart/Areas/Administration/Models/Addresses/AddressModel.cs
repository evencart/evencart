using EvenCart.Data.Entity.Addresses;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Addresses
{
    public class AddressModel : FoundationEntityModel, IRequiresValidations<AddressModel>
    {
        public int UserId { get; set; }

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

        public AddressType AddressType { get; set; }

        public string AddressTypeDisplay => AddressType.ToString();

        public void SetupValidationRules(ModelValidator<AddressModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Address1).NotEmpty();
            v.RuleFor(x => x.City).NotEmpty();
            v.RuleFor(x => x.CountryId).NotEmpty();
            v.RuleFor(x => x.ZipPostalCode).NotEmpty();
        }
    }
}