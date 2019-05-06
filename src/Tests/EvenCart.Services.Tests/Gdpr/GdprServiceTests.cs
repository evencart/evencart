using System;
using System.Collections.Generic;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Gdpr;
using EvenCart.Services.Users;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace EvenCart.Services.Tests.Gdpr
{
    public abstract class GdprServiceTests : BaseTest
    {
        private IGdprService _gdprService;
        private IUserService _userService;
        private IConsentService _consentService;
        private IConsentGroupService _consentGroupService;
        
        [SetUp]
        public void Setup()
        {
            _gdprService = Resolve<IGdprService>();
            _userService = Resolve<IUserService>();
            _consentService = Resolve<IConsentService>();
            _consentGroupService = Resolve<IConsentGroupService>();
        }

        [Test]
        public void User_Consents_Tests_Succeeds()
        {
            var user = new User()
            {
                Name = "Test User",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                LastLoginDate = null,
                Active = true
            };
            _userService.Insert(user);

            var consentGroup = new ConsentGroup()
            {
                Name = "Test Consent Group",
                Description = "Test"
            };
            _consentGroupService.Insert(consentGroup);

            var consents = new List<Consent>()
            {
                new Consent()
                {
                    Title = "Group Consent",
                    ConsentGroupId = consentGroup.Id,
                    Published = true
                },
                new Consent()
                {
                    Title = "Group Consent 2",
                    ConsentGroupId = consentGroup.Id,
                    IsRequired = true,
                    Published = true
                },
                new Consent()
                {
                    Title = "Default Consent",
                    OneTimeSelection = true,
                    Published = true
                }
            };
            _consentService.Insert(consents.ToArray());

            _gdprService.SetUserConsents(user.Id, new Dictionary<int, ConsentStatus>()
            {
                { consents[0].Id, ConsentStatus.Denied },
                { consents[1].Id, ConsentStatus.Accepted },
            });

            var userConsents = _gdprService.GetUserConsents(user.Id);
            Assert.AreEqual(2, userConsents.Count);

            Assert.IsFalse(_gdprService.IsConsentAccepted(user.Id, consents[0].Id));
            Assert.IsTrue(_gdprService.IsConsentAccepted(user.Id, consents[1].Id));
            Assert.IsFalse(_gdprService.IsConsentAccepted(user.Id, consents[2].Id));

            Assert.IsTrue(_gdprService.AreConsentsActedUpon(user.Id, consents[0].Id, consents[1].Id));
            Assert.IsFalse(_gdprService.AreConsentsActedUpon(user.Id, consents[0].Id, consents[1].Id, consents[2].Id));


            var pendingConsents = _gdprService.GetPendingConsents(user.Id);
            Assert.AreEqual(1, pendingConsents.Count);
        }
    }

    [TestFixture]
    public class SqlServerGdprServiceTests : GdprServiceTests
    {
        public SqlServerGdprServiceTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }

    [TestFixture]
    public class MySqlGdprServiceTests : GdprServiceTests
    {
        public MySqlGdprServiceTests()
        {
            TestDbInit.MySql(MySqlConnectionString);
        }
    }
}