using System.Linq;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Subscriptions;
using EvenCart.Services.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Manages subscriptions for a user
    /// </summary>
    public class SubscriptionController : FoundationController
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// Subscribes the logged in user or email provided for a particular subscription identifier
        /// </summary>
        /// <response code="200">A success or failure response object</response>
        [DualPost("subscribe", Name = RouteNames.SaveSubscription, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SubscriptionModel))]
        public IActionResult Subscribe(SubscriptionModel subscriptionModel)
        {
            //get subscription
            if (!_subscriptionService.GetAvailableSubscriptionRegistrars()
                .TryGetValue(subscriptionModel.SubscriptionGuid, out var subscription))
            {
                return R.Fail.With("error", T("Unknown subscription identifier")).Result;
            }

            if (!subscription.Second.ValidateData(subscriptionModel.SubscriptionGuid, subscriptionModel.Data))
            {
                return R.Fail.With("error", T("Invalid data provided for subscription")).Result;
            }
            
            if(subscriptionModel.Email.IsNullEmptyOrWhiteSpace())
                _subscriptionService.Subscribe(CurrentUser.Id, subscriptionModel.SubscriptionGuid, subscriptionModel.Data);
            else
                _subscriptionService.Subscribe(subscriptionModel.Email, subscriptionModel.SubscriptionGuid, subscriptionModel.Data);

            return R.Success.Result;
        }

        /// <summary>
        /// Unsubscribes the logged in user or the email provided for a particular subscription identifier
        /// </summary>
        /// <response code="200">A success or failure response object</response>
        [DualPost("unsubscribe", Name = RouteNames.DeleteSubscription, OnlyApi = true)]
        [ValidateModelState(ModelType = typeof(SubscriptionModel))]
        public IActionResult Unsubscribe(SubscriptionModel subscriptionModel)
        {
            //get subscription
            if (!_subscriptionService.GetAvailableSubscriptionRegistrars()
                .TryGetValue(subscriptionModel.SubscriptionGuid, out var subscription))
            {
                return R.Fail.With("error", T("Unknown subscription identifier")).Result;
            }

            if (!subscription.Second.ValidateData(subscriptionModel.SubscriptionGuid, subscriptionModel.Data))
            {
                return R.Fail.With("error", T("Invalid data provided for subscription")).Result;
            }

            if (subscriptionModel.Email.IsNullEmptyOrWhiteSpace())
                _subscriptionService.Unsubscribe(CurrentUser.Id, subscriptionModel.SubscriptionGuid, subscriptionModel.Data);
            else
                _subscriptionService.Unsubscribe(subscriptionModel.Email, subscriptionModel.SubscriptionGuid, subscriptionModel.Data);

            return R.Success.Result;
        }
    }
}