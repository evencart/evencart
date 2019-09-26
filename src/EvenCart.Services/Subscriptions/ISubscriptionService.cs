using System.Collections.Generic;
using EvenCart.Core.DataStructures;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Subscriptions;
using EvenCart.Data.Entity.Users;

namespace EvenCart.Services.Subscriptions
{
    public interface ISubscriptionService : IFoundationEntityService<Subscription>
    {
        void Subscribe(int userId, string subscriptionGuid, object data);

        void Subscribe(string email, string subscriptionGuid, object data);

        void Unsubscribe(int userId, string subscriptionGuid, object data);

        void Unsubscribe(string email, string subscriptionGuid, object data);

        IList<User> GetSubscribers(string subscriptionGuid, object data);

        IDictionary<string, Pair<string, ISubscriptionRegistrar>> GetAvailableSubscriptionRegistrars();

        bool IsSubscribed(int userId, string subscriptionGuid, object data);

        bool IsSubscribed(string email, string subscriptionGuid, object data);
    }
}