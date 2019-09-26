using EvenCart.Core.Infrastructure;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc.Models;
using EvenCart.Infrastructure.Mvc.Validator;
using EvenCart.Services.Extensions;
using EvenCart.Services.Subscriptions;
using FluentValidation;

namespace EvenCart.Models.Subscriptions
{
    /// <summary>
    /// Represents a single subscription 
    /// </summary>
    public class SubscriptionModel : FoundationModel, IRequiresValidations<SubscriptionModel>
    {
        public string SubscriptionGuid { get; set; }

        public string Email { get; set; }

        public string Data { get; set; }

        public void SetupValidationRules(ModelValidator<SubscriptionModel> v)
        {
            v.RuleFor(x => x.SubscriptionGuid).NotEmpty();
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress().When(x => ApplicationEngine.CurrentUser.IsVisitor());
        }
    }
}