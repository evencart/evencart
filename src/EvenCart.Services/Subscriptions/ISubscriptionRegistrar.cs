using System.Collections.Generic;
using EvenCart.Core.DataStructures;

namespace EvenCart.Services.Subscriptions
{
    public interface ISubscriptionRegistrar
    {
        IDictionary<string, string> GetSubscriptionTypes();

        bool ValidateData(string subscriptionGuid, string data);
    }
}