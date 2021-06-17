using Genesis.Modules.Users;
using NUnit.Framework;

namespace EvenCart.Services.Tests.Users
{
    public abstract class UserAuthenticationTests : BaseTest
    {
        private IUserRegistrationService _userRegistrationService;

        [SetUp]
        public void Setup()
        {
            _userRegistrationService = Resolve<IUserRegistrationService>();
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
}