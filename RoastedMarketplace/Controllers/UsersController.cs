using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.Attributes;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Users;
using RoastedMarketplace.Services.Users;

namespace RoastedMarketplace.Controllers
{
    [Route("users")]
    [Authorize]
    public class UsersController : FoundationController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

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
            _userService.Update(currentUser);

            return R.Success.Result;
        }
    }
}