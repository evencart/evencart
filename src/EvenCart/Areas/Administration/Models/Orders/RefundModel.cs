using EvenCart.Data.Entity.Purchases;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using FluentValidation;

namespace EvenCart.Areas.Administration.Models.Orders
{
    public class RefundModel : FoundationModel, IRequiresValidations<RefundModel>
    {
        public bool RefundOffline { get; set; }

        public bool IsPartialRefund { get; set; }

        public decimal Amount { get; set; }

        public int OrderId { get; set; }

        public RefundType RefundType { get; set; }

        public void SetupValidationRules(ModelValidator<RefundModel> v)
        {
            v.RuleFor(x => x.Amount).GreaterThan(0).When(x => x.IsPartialRefund);
            v.RuleFor(x => x.OrderId).GreaterThan(0);
        }
    }
}