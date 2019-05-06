using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Mvc.ModelFactories;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : FoundationController
    {

        private readonly IModelMapper _modelMapper;

        public AccountController(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }


        [DualGet("", Name = RouteNames.AccountProfile)]
        public IActionResult Profile()
        {
            var currentUser = ApplicationEngine.CurrentUser;
            var userModel = _modelMapper.Map<UserModel>(currentUser);
            return R.Success.With("user", userModel).Result;
        }
    }
}