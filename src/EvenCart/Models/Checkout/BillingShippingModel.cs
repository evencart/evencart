using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using EvenCart.Models.Addresses;
using FluentValidation;

namespace EvenCart.Models.Checkout
{
    public class BillingShippingModel : FoundationModel, IRequiresValidations<BillingShippingModel>
    {
        /// <summary>
        /// Set to true to use a different shipping address than the billing address
        /// </summary>
        public bool UseDifferentShippingAddress { get; set; }

        /// <summary>
        /// The <see cref="AddressInfoModel">billingAddress</see> of the user
        /// </summary>
        public AddressInfoModel BillingAddress { get; set; }

        /// <summary>
        /// The <see cref="AddressInfoModel">shippingAddress</see> of the user. Ignore if useDifferentShippingAddress is set to false.
        /// </summary>
        public AddressInfoModel ShippingAddress { get; set; }

        /// <summary>
        /// The <see cref="ShippingMethodModel">shippingMethod</see> to be used. Ignore if not applicable.
        /// </summary>
        public ShippingMethodModel ShippingMethod { get; set; }

        public void SetupValidationRules(ModelValidator<BillingShippingModel> v)
        {
            v.RuleFor(x => x.BillingAddress).NotNull();
            v.RuleFor(x => x.BillingAddress.Name).NotEmpty().When(x => x.BillingAddress.Id == 0);
            v.RuleFor(x => x.BillingAddress.Address1).NotEmpty().When(x => x.BillingAddress.Id == 0);
            v.RuleFor(x => x.BillingAddress.City).NotEmpty().When(x => x.BillingAddress.Id == 0);
            v.RuleFor(x => x.BillingAddress.StateProvinceName)
                .NotEmpty()
                .When(x => x.BillingAddress.Id == 0 && !(x.BillingAddress.StateProvinceId.HasValue && x.BillingAddress.StateProvinceId.Value > 0));
            v.RuleFor(x => x.BillingAddress.CountryId).GreaterThan(0).When(x => x.BillingAddress.Id == 0);
            v.RuleFor(x => x.BillingAddress.Email).NotEmpty().EmailAddress().When(x => x.BillingAddress.Id == 0);
            v.RuleFor(x => x.BillingAddress.ZipPostalCode).NotEmpty().When(x => x.BillingAddress.Id == 0);

            v.RuleFor(x => x.ShippingAddress).NotNull().When(x => x.UseDifferentShippingAddress);
            v.RuleFor(x => x.ShippingAddress.Name).NotEmpty().When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
            v.RuleFor(x => x.ShippingAddress.Address1).NotEmpty().When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
            v.RuleFor(x => x.ShippingAddress.City).NotEmpty().When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
            v.RuleFor(x => x.ShippingAddress.StateProvinceName)
                .NotEmpty()
                .When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0 && !(x.ShippingAddress.StateProvinceId.HasValue && x.ShippingAddress.StateProvinceId.Value > 0));
            v.RuleFor(x => x.ShippingAddress.CountryId).GreaterThan(0).When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
            v.RuleFor(x => x.ShippingAddress.Email).NotEmpty().EmailAddress().When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
            v.RuleFor(x => x.ShippingAddress.ZipPostalCode).NotEmpty().When(x => x.UseDifferentShippingAddress && x.ShippingAddress.Id == 0);
        }
    }
}