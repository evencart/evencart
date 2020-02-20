using System.Collections.Generic;

namespace EvenCart.Services.Subscriptions
{
    public interface ISubscriptionRegistrar
    {
        IDictionary<string, string> GetSubscriptionTypes();

        bool ValidateData(string subscriptionGuid, string data);
    }
}