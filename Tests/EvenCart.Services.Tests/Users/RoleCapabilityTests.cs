using System;
using System.Linq;
using EvenCart.Data.Constants;
using EvenCart.Data.Entity.Users;
using EvenCart.Services.Extensions;
using EvenCart.Services.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Users
{
    public abstract class RoleCapabilityTests : BaseTest
    {
        private IRoleService _roleService;
        private IUserService _userService;
        private IRoleCapabilityService _roleCapabilityService;
        private ICapabilityService _capabilityService;

        [SetUp]
        protected void Setup()
        {
            _roleService = Resolve<IRoleService>();
            _userService = Resolve<IUserService>();
            _roleCapabilityService = Resolve<IRoleCapabilityService>();
            _capabilityService = Resolve<ICapabilityService>();
        }

        [Test]
        public void Role_capability_tests_Succeeds()
        {
            var role1 = new Role()
            {
                Name = "Test Role",
                SystemName = "TestRole",
                IsActive = true,
                IsSystemRole = false
            };
            _roleService.Insert(role1);

            var role2 = new Role()
            {
                Name = "Test Role 2",
                SystemName = "TestRole",
                IsActive = true,
                IsSystemRole = false
            };
            _roleService.Insert(role2);

            var accessAdminCap =
                _capabilityService.FirstOrDefault(x => x.Name == CapabilitySystemNames.AccessAdministration);
            var createOrderCap =
                _capabilityService.FirstOrDefault(x => x.Name == CapabilitySystemNames.CreateOrder);
            var editProductCap =
                _capabilityService.FirstOrDefault(x => x.Name == CapabilitySystemNames.EditProduct);

            var user = new User()
            {
                Name = "Test User",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                LastLoginDate = null,
                Active = true
            };
            _userService.Insert(user);


            _capabilityService.SetRoleCapabilities(role1.Id, new []{ accessAdminCap.Id });
            _capabilityService.SetRoleCapabilities(role2.Id, new []{ editProductCap.Id });
            _capabilityService.SetUserCapabilities(user.Id, new[] {createOrderCap.Id});

            _roleService.SetUserRoles(user.Id, new []{ role1.Id, role2.Id });
            //refetch to load all data
            user = _userService.Get(user.Id);

            var consoliatedCapabilities = _capabilityService.GetByRolesConsolidated(new[] {role1.Id, role2.Id});
            Assert.AreEqual(2, consoliatedCapabilities.Count());

            Assert.IsTrue(user.Can(CapabilitySystemNames.AccessAdministration));
            Assert.IsTrue(user.Can(CapabilitySystemNames.EditProduct));
            Assert.IsTrue(user.Can(CapabilitySystemNames.CreateOrder));
            Assert.IsFalse(user.Can(CapabilitySystemNames.DeleteUser));
        }
    }

    [TestFixture]
    public class SqlServerRoleCapabilityTests : RoleCapabilityTests
    {
        public SqlServerRoleCapabilityTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}