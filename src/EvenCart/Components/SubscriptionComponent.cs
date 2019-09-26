using System;
using System.Collections.Generic;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;
using EvenCart.Services.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "Subscription")]
    public class SubscriptionComponent : FoundationComponent
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionComponent(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public override IViewComponentResult Invoke(object data = null)
        {
            var dataAsDict = data as Dictionary<string, object>;
            if (dataAsDict == null)
                return R.Success.ComponentResult;
            dataAsDict.TryGetValue("subscriptionGuid", out var subscriptionGuid);
            dataAsDict.TryGetValue("data", out var subData);

            var subscriptionGuidAsStr = subscriptionGuid?.ToString();
            if (subscriptionGuidAsStr.IsNullEmptyOrWhiteSpace())
                throw new Exception("The subscription guid must be a valid subscription id");
            if (!_subscriptionService.GetAvailableSubscriptionRegistrars().ContainsKey(subscriptionGuidAsStr))
                throw new Exception("The subscription guid must be a valid subscription id");
            var currentUser = ApplicationEngine.CurrentUser;

            var isSubscribed = !currentUser.IsVisitor() && _subscriptionService.IsSubscribed(currentUser.Id, subscriptionGuidAsStr, subData);
            var url = isSubscribed
                ? ApplicationEngine.RouteUrl("api_" + RouteNames.DeleteSubscription)
                : ApplicationEngine.RouteUrl("api_" + RouteNames.SaveSubscription);

            return R.Success
                .With("subscriptionGuid", subscriptionGuidAsStr)
                .With("data", subData)
                .With("url", url)
                .With("isSubscribed", isSubscribed)
                .ComponentResult;
        }
    }
}