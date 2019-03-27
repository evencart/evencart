using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoastedMarketplace.Infrastructure;
using RoastedMarketplace.Infrastructure.Mvc;
using RoastedMarketplace.Infrastructure.Mvc.ModelFactories;
using RoastedMarketplace.Infrastructure.Routing;
using RoastedMarketplace.Models.Users;

namespace RoastedMarketplace.Controllers
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