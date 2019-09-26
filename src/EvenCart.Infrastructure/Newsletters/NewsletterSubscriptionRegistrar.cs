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