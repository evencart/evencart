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

using EvenCart.Factories.Users;
using EvenCart.Models.Users;
using Genesis.Infrastructure.Mvc;
using Genesis.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated user to manage personal information
    /// </summary>
    [Route("account")]
    [Authorize]
    public class AccountController : GenesisController
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
            var currentUser = CurrentUser;
            var userModel = _userModelFactory.Create(currentUser);
            return R.Success.With("user", userModel).WithTimezones().Result;
        }
    }
}