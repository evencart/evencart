﻿#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Linq;
using EvenCart.Models.ConnectedAccounts;
using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Users;
using Genesis.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Manages connected accounts for a user
    /// </summary>
    [Authorize]
    [Route("connected-accounts")]
    public class ConnectedAccountsController : GenesisController
    {
        private readonly IConnectedAccountService _connectedAccountService;

        public ConnectedAccountsController(IConnectedAccountService connectedAccountService)
        {
            _connectedAccountService = connectedAccountService;
        }
        /// <summary>
        /// Returns the list of connected accounts to the logged in user
        /// </summary>
        /// <response code="200">A list of <see cref="ConnectedAccountModel">connected accounts</see> as 'connectedAccounts'</response>
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