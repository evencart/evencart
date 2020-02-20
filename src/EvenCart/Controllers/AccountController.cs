using EvenCart.Factories.Users;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated user to manage personal information
    /// </summary>
    [Route("account")]
    [Authorize]
    public class AccountController : FoundationController
    {

        private readonly IUserModelFactory _userModelFactory;
        public AccountController(IUserModelFactory userModelFactory)
        {
            _userModelFactory = userModelFactory;
        }

        /// <summary>
        /// Gets the account information for authenticated user
        /// </summary>
        /// <response code="200">The <see cref="UserModel">user</see> object</response>
        [DualGet("", Name = RouteNames.AccountProfile)]
        public IActionResult Profile()
        {
            var currentUser = ApplicationEngine.CurrentUser;
            var userModel = _userModelFactory.Create(currentUser);
            return R.Success.With("user", userModel).WithTimezones().Result;
        }
    }
}