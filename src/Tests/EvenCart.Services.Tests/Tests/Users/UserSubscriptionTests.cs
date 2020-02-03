using System;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Subscriptions;
using EvenCart.Services.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Users
{
    public abstract class UserSubscriptionTests : BaseTest
    {
        private ISubscriptionService _subscriptionService;
        private IUserService _userService;
        private const string SubscriptionId1 = "8BA0258F-6162-45A4-BC6C-DC3D2A82384D";
        private const string SubscriptionId2 = "DCF2ECBB-3FE4-4975-AD6C-6645FBEE3B0D";
        [SetUp]
        public void Setup()
        {
            _subscriptionService = Resolve<ISubscriptionService>();
            _userService = Resolve<IUserService>();
        }

        [Test]
        public void User_Subscription_Succeeds()
        {
            var user1 = new User()
            {
                Name = "Test User 1",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                LastLoginDate = null,
                Active = true,
                Email = "test_user_sub_1@email.com"
            };
            _userService.Insert(user1);

            var user2 = new User()
            {
                Name = "Test User 1",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                LastLoginDate = null,
                Active = false,
                Email = "test_user_sub_2@email.com"
            };
            _userService.Insert(user2);

            _subscriptionService.Subscribe(user1.Id, SubscriptionId1, null);
            _subscriptionService.Subscribe(user1.Id, SubscriptionId2, 5);

            _subscriptionService.Subscribe(user2.Id, SubscriptionId1, 5);
            _subscriptionService.Subscribe(user2.Id, SubscriptionId2, 5);


            Assert.IsTrue(_subscriptionService.IsSubscribed(user1.Id, SubscriptionId1, null));
            Assert.IsFalse(_subscriptionService.IsSubscribed(user1.Id, SubscriptionId1, 5));
            Assert.IsTrue(_subscriptionService.IsSubscribed(user1.Id, SubscriptionId2, 5));
            Assert.IsFalse(_subscriptionService.IsSubscribed(user1.Id, "NON-EXISTENT", 5));

            Assert.IsTrue(_subscriptionService.IsSubscribed(user2.Id, SubscriptionId1, 5));
            Assert.IsTrue(_subscriptionService.IsSubscribed(user2.Id, SubscriptionId2, 5));
            Assert.IsFalse(_subscriptionService.IsSubscribed(user2.Id, SubscriptionId2, null));

            var subscribers = _subscriptionService.GetSubscribers(SubscriptionId1, null);
            Assert.AreEqual(1, subscribers.Count);
            Assert.AreEqual(user1.Email, subscribers[0].Email);

            subscribers = _subscriptionService.GetSubscribers(SubscriptionId1, 5);
            Assert.AreEqual(0, subscribers.Count);

            subscribers = _subscriptionService.GetSubscribers(SubscriptionId2, 5);
            Assert.AreEqual(1, subscribers.Count);
            Assert.AreEqual(user1.Email, subscribers[0].Email);
        }
    }
}