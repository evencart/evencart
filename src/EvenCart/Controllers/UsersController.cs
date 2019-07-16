using EvenCart.Services.Users;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.Attributes;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows authenticated user to manage their personal info
    /// </summary>
    [Route("users")]
    [Authorize]
    public class UsersController : FoundationController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Saves authenticated user's information
        /// </summary>
        /// <param name="userModel"></param>
        /// <response code="200">A success response object</response>
        [ValidateModelState(ModelType = typeof(UserModel))]
        [DualPost("", Name = RouteNames.SaveUser, OnlyApi = true)]
        public IActionResult SaveUser(UserModel userModel)
        {
            var currentUser = ApplicationEngine.CurrentUser;
            //set the updated fields
            currentUser.FirstName = userModel.FirstName;
            currentUser.LastName = userModel.LastName;
            currentUser.Name = $"{currentUser.FirstName} {currentUser.LastName}";
            currentUser.CompanyName = userModel.CompanyName;
            currentUser.DateOfBirth = userModel.DateOfBirth;
            currentUser.MobileNumber = userModel.MobileNumber;
            currentUser.TimeZoneId = userModel.TimeZoneId;
            _userService.Update(currentUser);

            return R.Success.Result;
        }
    }
}