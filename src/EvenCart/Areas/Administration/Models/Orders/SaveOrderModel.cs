using EvenCart.Data.Entity.Payments;
using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class SaveOrderModel : FoundationEntityModel, IRequiresValidations<SaveOrderModel>
    {
        public string ShippingMethodName { get; set; }

        public string ShippingMethodDisplayName { get; set; }

        public string SelectedShippingOption { get; set; }

        public string PaymentMethodName { get; set; }

        public string PaymentMethodDisplayName { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Subtotal { get; set; }

        public decimal? ShippingMethodFee { get; set; }

        public decimal? PaymentMethodFee { get; set; }

        public decimal? Tax { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        public void SetupValidationRules(ModelValidator<SaveOrderModel> v)
        {
            v.RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}