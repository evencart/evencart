#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using EvenCart.Models.Subscriptions;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc;
using Genesis.Infrastructure.Mvc.Attributes;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Manages subscriptions for a user
    /// </summary>
    public class SubscriptionController : GenesisController
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