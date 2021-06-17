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

using System;
using System.Collections.Generic;
using Genesis.Extensions;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Components
{
    [ViewComponent(Name = "Subscription")]
    public class SubscriptionComponent : GenesisComponent
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
            var currentUser = Engine.CurrentUser;

            var isSubscribed = !currentUser.IsVisitor() && _subscriptionService.IsSubscribed(currentUser.Id, subscriptionGuidAsStr, subData);
            var url = isSubscribed
                ? Engine.RouteUrl("api_" + RouteNames.DeleteSubscription)
                : Engine.RouteUrl("api_" + RouteNames.SaveSubscription);

            return R.Success
                .With("subscriptionGuid", subscriptionGuidAsStr)
                .With("data", subData)
                .With("url", url)
                .With("isSubscribed", isSubscribed)
                .ComponentResult;
        }
    }
}