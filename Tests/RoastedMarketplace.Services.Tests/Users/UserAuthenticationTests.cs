using NUnit.Framework;
using RoastedMarketplace.Data.Enum;
using RoastedMarketplace.Services.Authentication;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Services.Tests.Users
{
    public abstract class UserAuthenticationTests : BaseTest
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserService _userService;

        public UserAuthenticationTests()
        {
            _userRegistrationService = Resolve<IUserRegistrationService>();
            _userService = Resolve<IUserService>();
        }

        [Test]
        public void User_Registration_Tests_Succeed()
        {
            var status = _userRegistrationService.Register("user@myemail.com", "somerandom", PasswordFormat.Md5Hashed);
            Assert.AreEqual(UserRegistrationStatus.Success, status);

            status = _userRegistrationService.Register("user@myemail.com", "somerandomaer", PasswordFormat.Md5Hashed);
            Assert.AreEqual(UserRegistrationStatus.FailedAsEmailAlreadyExists, status);
        }

        [Test]
        public void User_Login_Tests_Succeed()
        {
            //todo: see how this will work
            /*
            var _appAuthenticationService = Resolve<IAppAuthenticationService>();


            var status = _appAuthenticationService.SignIn("login@myemail.com");
            Assert.AreEqual(LoginStatus.FailedUserNotExists, status);

            status = _appAuthenticationService.SignIn("login@myemail.com", forceCreateNewAccount: true);
            Assert.AreEqual(LoginStatus.Success, status);

            var cu = _appAuthenticationService.GetCurrentUser();
            Assert.AreEqual("login@myemail.com", cu.Email);

            cu.Active = false;
            _userService.Update(cu);
            status = _appAuthenticationService.SignIn("login@myemail.com");
            Assert.AreEqual(LoginStatus.FailedInactiveUser, status);

            cu.Deleted = true;
            status = _appAuthenticationService.SignIn("login@myemail.com");
            Assert.AreEqual(LoginStatus.FailedDeletedUser, status);*/
        }


    }

    [TestFixture]
    public class SqlServerUserAuthenticationTests : UserAuthenticationTests
    {
        public SqlServerUserAuthenticationTests()
        {
            TestDbInit.SqlServer(MsSqlConnectionString);
        }
    }
}