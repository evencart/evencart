using System;
using System.Collections.Generic;
using System.Linq;
using DotEntity;
using EvenCart.Core.DataStructures;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Core.Services;
using EvenCart.Data.Entity.Subscriptions;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Serializers;

namespace EvenCart.Services.Subscriptions
{
    public class SubscriptionService : FoundationEntityService<Subscription>, ISubscriptionService
    {
        private readonly IDataSerializer _dataSerializer;
        public SubscriptionService(IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
        }

        public void Subscribe(int userId, string subscriptionGuid, object data)
        {
            if (!IsSubscribed(userId, subscriptionGuid, data))
                InsertImpl(userId, null, subscriptionGuid, data);
        }

        public void Subscribe(string email, string subscriptionGuid, object data)
        {
            if (!IsSubscribed(email, subscriptionGuid, data))
                InsertImpl(null, email, subscriptionGuid, data);
        }


        public void Unsubscribe(int userId, string subscriptionGuid, object data)
        {
            var dataSerialized = Serialized(data);
            Delete(x => x.SubscriptionGuid == subscriptionGuid && x.UserId == userId && x.Data == dataSerialized);
        }

        public void Unsubscribe(string email, string subscriptionGuid, object data)
        {
            var dataSerialized = Serialized(data);
            Delete(x => x.SubscriptionGuid == subscriptionGuid && x.Email == email && x.Data == dataSerialized);
        }

        public IList<User> GetSubscribers(string subscriptionGuid, object data)
        {
            var dataSerialized = Serialized(data);
            var userTable = DotEntityDb.GetTableNameForType<User>();
            var subscriptionTable = DotEntityDb.GetTableNameForType<Subscription>();

            var idCol = DotEntityDb.Provider.SafeEnclose(nameof(User.Id));
            var userIdCol = DotEntityDb.Provider.SafeEnclose(nameof(Subscription.UserId));
            var emailCol = DotEntityDb.Provider.SafeEnclose(nameof(Subscription.Email));
            var guidCol = DotEntityDb.Provider.SafeEnclose(nameof(Subscription.SubscriptionGuid));
            var dataCol = DotEntityDb.Provider.SafeEnclose(nameof(Subscription.Data));

            var query =
                $"SELECT * FROM {userTable} WHERE {idCol} IN (SELECT {userIdCol} FROM {subscriptionTable} WHERE {guidCol}=@subscriptionGuid AND {dataCol}=@data WHERE {userIdCol} IS NOT NULL) OR " +
                $"{emailCol} IN (SELECT {emailCol} FROM {subscriptionTable} WHERE {guidCol}=@subscriptionGuid AND {dataCol}=@data WHERE {emailCol} IS NOT NULL)";


            return RepositoryExplorer<User>().QueryNested(query, new {subscriptionGuid, data = dataSerialized})
                .ToList();

            //Repository.Join<User>("Email", "Email")
            //    .Join<User>("UserId", "Id", SourceColumn.Parent)
            //    .Relate<User>((subscription, user) =>
            //    {
            //        if(!users.Contains(user))
            //            users.Add(user);
            //    })
            //    .Where(x => x.SubscriptionGuid == subscriptionGuid && x.Data == dataSerialized)
            //    .SelectNested();
            //return users;
        }

        private IDictionary<string, Pair<string, ISubscriptionRegistrar>> registrarDictionary;
        public IDictionary<string, Pair<string, ISubscriptionRegistrar>> GetAvailableSubscriptionRegistrars()
        {
            if (registrarDictionary != null)
                return registrarDictionary;
            var registrars = TypeFinder.InstancesOfType<ISubscriptionRegistrar>();
            registrarDictionary = registrarDictionary ?? new Dictionary<string, Pair<string, ISubscriptionRegistrar>>();
            if (registrars != null && registrars.Any())
            {
                foreach (var r in registrars)
                {
                    var subscriptionTypes = r.GetSubscriptionTypes();
                    foreach (var (key, value) in subscriptionTypes)
                        registrarDictionary.Add(key, Pair.Create(value, r));

                }
            }

            return registrarDictionary;
        }

        public bool IsSubscribed(int userId, string subscriptionGuid, object data)
        {
            var dataSerialized = Serialized(data);
            var count = Count(x => x.UserId == userId && x.SubscriptionGuid == subscriptionGuid &&
                              x.Data == dataSerialized);
            return count > 0;
        }

        public bool IsSubscribed(string email, string subscriptionGuid, object data)
        {
            var dataSerialized = Serialized(data);
            var count = Count(x => x.Email == email && x.SubscriptionGuid == subscriptionGuid &&
                              x.Data == dataSerialized);
            return count > 0;
        }

        #region Helpers
        private void InsertImpl(int? userId, string email, string subscriptionGuid, object data)
        {
            Insert(new Subscription()
            {
                CreatedOn = DateTime.UtcNow,
                Data = Serialized(data),
                Email = email,
                UserId = userId,
                SubscriptionGuid = subscriptionGuid
            });
        }

        private string Serialized(object data)
        {
            if (data == null)
                return null;
            if (data.GetType().IsPrimitive)
                data = data.ToString();
            return _dataSerializer.Serialize(data);
        }
        #endregion
    }
}