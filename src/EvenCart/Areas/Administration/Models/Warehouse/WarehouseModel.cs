using EvenCart.Areas.Administration.Models.Addresses;
using EvenCart.Areas.Administration.Models.Users;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    public class WarehouseModel : FoundationEntityModel, IRequiresValidations<WarehouseModel>
    {
        /// <summary>
        /// The address details of the warehouse
        /// </summary>
        public AddressModel Address { get; set; }

        public void SetupValidationRules(ModelValidator<WarehouseModel> v)
        {
            v.RuleFor(x => x.Address).NotNull();
            v.RuleFor(x => x.Address.Name).NotEmpty();
            v.RuleFor(x => x.Address.Address1).NotEmpty();
            v.RuleFor(x => x.Address.City).NotEmpty();
            v.RuleFor(x => x.Address.CountryId).NotEmpty();
            v.RuleFor(x => x.Address.ZipPostalCode).NotEmpty();
        }
    }
}