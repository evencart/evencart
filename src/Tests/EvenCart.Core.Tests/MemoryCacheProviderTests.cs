using System;
using System.Threading;
using Genesis.Caching;
using NUnit.Framework;

namespace EvenCart.Common.Tests
{
    [TestFixture]
    public class MemoryCacheProviderTests
    {
        [Test]
        public void Memory_Cache_Simple_Tests_Succeed()
        {
            ICacheProvider provider = new MemoryCacheProvider();

            var user = provider.Get<User>("user_1");
            Assert.IsNull(user);

            var user1 = provider.Get("user_1", () => new User(1, Guid.NewGuid()));
            var user2 = provider.Get("user_1", () => new User(1, Guid.NewGuid()));
            var user3 = provider.Get("user_3", () => new User(1, Guid.NewGuid()));
            Assert.AreEqual(user1.Guid, user2.Guid);
            Assert.AreNotEqual(user1.Guid, user3.Guid);

            Assert.IsFalse(provider.IsSet("user_4"));
            Assert.IsTrue(provider.IsSet("user_3"));

            provider.Remove("user_1");
            Assert.IsFalse(provider.IsSet("user_1"));

            provider.Clear();
            Assert.IsFalse(provider.IsSet("user_2"));
            Assert.IsFalse(provider.IsSet("user_3"));
        }

        [Test]
        public void Memory_Cache_Expiration_Tests_Succeed()
        {
            ICacheProvider provider = new MemoryCacheProvider();

            var user1 = provider.Get("user_1", () => new User(1, Guid.NewGuid()), 1);//expiration time of one minute
            Assert.IsNotNull(user1);

            var user1Reloaded = provider.Get("user_1", () => new User(1, Guid.NewGuid()), 1);

            Assert.AreSame(user1, user1Reloaded);

            Thread.Sleep(60 * 1000); //one minute sleep
            user1Reloaded = provider.Get("user_1", () => new User(1, Guid.NewGuid()), 1);

            Assert.AreNotSame(user1, user1Reloaded);
        }
    }

    public class User
    {
        public User(int id, Guid guid)
        {
            Id = id;
            Guid = guid.ToString();
        }
        public int Id { get; set; }

        public string Guid { get; set; }
    }
}