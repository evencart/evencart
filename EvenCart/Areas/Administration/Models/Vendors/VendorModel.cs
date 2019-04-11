using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Vendors
{
    public class VendorModel : FoundationEntityModel, IRequiresValidations<VendorModel>
    {
        public string Name { get; set; }

        public string GstNumber { get; set; }

        public string Tin { get; set; }

        public string Pan { get; set; }

        public string Address { get; set; }

        public int? StateProvinceId { get; set; }

        public string StateProvinceName { get; set; }

        public string City { get; set; }

        public int CountryId { get; set; }

        public string ZipPostalCode { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public void SetupValidationRules(ModelValidator<VendorModel> v)
        {
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Address).NotEmpty();
        }
    }
}