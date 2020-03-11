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

using System.Collections.Generic;
using EvenCart.Services.Subscriptions;

namespace EvenCart.Infrastructure.Newsletters
{
    public class NewsletterSubscriptionRegistrar : ISubscriptionRegistrar
    {
        public IDictionary<string, string> GetSubscriptionTypes()
        {
            return new Dictionary<string, string>()
            {
                { ApplicationConfig.NewsletterSubscriptionGuid, "Newsletter Subscription" }
            };
        }

        public bool ValidateData(string subscriptionGuid, string data)
        {
            return true;
        }
    }
}