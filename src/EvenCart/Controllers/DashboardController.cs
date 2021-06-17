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

using Genesis.Infrastructure.Mvc;
using Genesis.Modules.Users;
using Genesis.Routing;
using Genesis.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows user to set global preferences
    /// </summary>
    [Route("")]
    public class DashboardController : GenesisController
    {
        private readonly IViewAccountant _viewAccountant;
        private readonly IUserService _userService;
        public DashboardController(IViewAccountant viewAccountant, IUserService userService)
        {
            _viewAccountant = viewAccountant;
            _userService = userService;
        }

        [DualGet("templates", Name = RouteNames.Templates, OnlyApi = true)]
        public IActionResult LoadTemplates(string context)
        {
            var templates = _viewAccountant.CompileAllViews(context, null, true);
            return R.Success.With("templates", templates).Result;
        }
        [DualPost("rawview", Name = RouteNames.RawView, OnlyApi = true)]
        public IActionResult GetRawView(string viewPath)
        {
            return R.Success.WithRawView(viewPath).Result;
        }
        /// <summary>
        /// Sets the active currency for user
        /// </summary>
        /// <param name="currencyId">The id of the currency</param>
        /// <response code="200">A success response object</response>
        [DualPost("set-active-currency", Name = RouteNames.SetActiveCurrency, OnlyApi = true)]
        public IActionResult SetActiveCurrency(int currencyId)
        {
            if (Engine.CurrentUser == null)
                Engine.GuestSignIn();

            Engine.CurrentUser.ActiveCurrencyId = currencyId;
            _userService.Update(Engine.CurrentUser);
            return R.Success.Result;
        }
        /// <summary>
        /// Sets the active language for user
        /// </summary>
        /// <param name="languageCulture">The culture of the language</param>
        /// <response code="200">A success response object</response>
        [DualPost("set-active-language", Name = RouteNames.SetActiveLanguage, OnlyApi = true)]
        public IActionResult SetActiveLanguage(string languageCulture)
        {
            if (string.IsNullOrWhiteSpace(languageCulture))
                return BadRequest();
            if (Engine.CurrentUser == null)
                Engine.GuestSignIn();

            Engine.CurrentUser.ActiveLanguageCulture = languageCulture;
            _userService.Update(Engine.CurrentUser);
            return R.Success.Result;
        }
    }
}