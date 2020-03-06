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