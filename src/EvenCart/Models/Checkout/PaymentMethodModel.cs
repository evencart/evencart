using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Checkout
{
    public class PaymentMethodModel : FoundationModel, IRequiresValidations<PaymentMethodModel>
    {
        /// <summary>
        /// The payment method system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// The friendly name for payment method. Ignored for POST requests.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// The description of payment method. Ignored for POST requests.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The additional fee for payment method. Ignored for POST requests.
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// The url to redirect for payment method processing. Ignored for POST requests.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The payment information in key-value format. 
        /// </summary>
        public IFormCollection FormCollection { get; set; }

        /// <summary>
        /// The order guid for which this payment method is being used. This is used only for retrying a previously failed order.
        /// </summary>
        public string OrderGuid { get; set; }

        /// <summary>
        /// Specifies if store credits should be used if available
        /// </summary>
        public bool UseStoreCredits { get; set; }

        public void SetupValidationRules(ModelValidator<PaymentMethodModel> v)
        {
            v.RuleFor(x => x.SystemName).NotEmpty().When(x => !x.UseStoreCredits);
        }
    }
}