using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Models.Checkout
{
    public class PaymentMethodModel : FoundationModel, IRequiresValidations<PaymentMethodModel>
    {
        public string SystemName { get; set; }

        public string FriendlyName { get; set; }

        public string Description { get; set; }

        public decimal Fee { get; set; }

        public string Url { get; set; }

        public IFormCollection FormCollection { get; set; }

        public string OrderGuid { get; set; }

        public void SetupValidationRules(ModelValidator<PaymentMethodModel> v)
        {
            v.RuleFor(x => x.SystemName).NotEmpty();
        }
    }
}