using System.Linq;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Models.ConnectedAccounts;
using EvenCart.Services.Social;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Manages connected accounts for a user
    /// </summary>
    [Authorize]
    [Route("connected-accounts")]
    public class ConnectedAccountsController : FoundationController
    {
        private readonly IConnectedAccountService _connectedAccountService;

        public ConnectedAccountsController(IConnectedAccountService connectedAccountService)
        {
            _connectedAccountService = connectedAccountService;
        }
        /// <summary>
        /// Returns the list of connected accounts to the logged in user
        /// </summary>
        /// <response>A list of <see cref="ConnectedAccountModel">connected accounts</see> as 'connectedAccounts'</response>
        [DualGet("~/account/connected-accounts", Name = RouteNames.AccountConnectedAccounts)]
        public IActionResult ConnectedAccountsList()
        {
            var connectedAccounts = _connectedAccountService.Get(x => x.UserId == CurrentUser.Id);
            var models = connectedAccounts.Select(x => new ConnectedAccountModel()
            {
                Id = x.Id,
                ProviderName = x.ProviderName
            }).ToList();

            return R.Success.With("connectedAccounts", models).Result;
        }

        /// <summary>
        /// Deletes a connected account for the logged in user
        /// </summary>
        /// <param name="id">The id of the connected account</param>
        /// <response code="200">A success response object on success</response>
        [DualPost("delete", Name = RouteNames.DeleteConnectedAccount, OnlyApi = true)]
        public IActionResult DeleteConnectedAccount(int id)
        {
            var connectedAccount = _connectedAccountService.FirstOrDefault(x => x.UserId == CurrentUser.Id && x.Id == id);
            if (connectedAccount == null)
                return NotFound();
            _connectedAccountService.Delete(connectedAccount);
            return R.Success.Result;
        }
    }
}