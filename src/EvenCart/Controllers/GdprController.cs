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

using System;
using System.Collections.Generic;
using System.Linq;
using EvenCart.Data.Entity.Gdpr;
using EvenCart.Services.Gdpr;
using EvenCart.Factories.Gdpr;
using EvenCart.Infrastructure;
using EvenCart.Infrastructure.Helpers;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Infrastructure.Routing;
using EvenCart.Infrastructure.Security.Attributes;
using EvenCart.Models.Gdpr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvenCart.Controllers
{
    /// <summary>
    /// Allows to manage GDPR settings for the authenticated user
    /// </summary>
    [Authorize]
    [Route("gdpr")]
    public class GdprController : FoundationController
    {
        private readonly IGdprService _gdprService;
        private readonly IConsentService _consentService;
        private readonly IGdprModelFactory _gdprModelFactory;
        public GdprController(IGdprService gdprService, IConsentService consentService, IGdprModelFactory gdprModelFactory)
        {
            _gdprService = gdprService;
            _consentService = consentService;
            _gdprModelFactory = gdprModelFactory;
        }

        /// <summary>
        /// Gets the current GDPR consents for authenticated user
        /// </summary>
        /// <response code="200">A list of <see cref="ConsentGroupModel">consentGroups</see> objects</response>
        [DualGet("~/account/privacy", Name = RouteNames.AccountGdpr)]
        public IActionResult Consents()
        {
            var userConsents = _gdprService.GetUserConsents(CurrentUser.Id);
            var allConsents = _consentService.Get(x => x.Published).GroupBy(x => x.ConsentGroup).ToList();
            var consentGroups = new List<ConsentGroupModel>();
            var acceptedIds = userConsents.Where(x => x.ConsentStatus == ConsentStatus.Accepted)
                .Select(x => x.ConsentId).ToArray();
            var deniedIds = userConsents.Where(x => x.ConsentStatus == ConsentStatus.Denied)
                .Select(x => x.ConsentId).ToArray();
            foreach (var ac in allConsents)
            {
                var group = ac.Key ?? new ConsentGroup()
                {
                    Consents = ac.ToList()
                };
                consentGroups.Add(_gdprModelFactory.Create(group, acceptedIds, deniedIds));
            }

            return R.Success.With("consentGroups", consentGroups).Result;
        }

        /// <summary>
        /// Saves the GDPR consents for authenticated users
        /// </summary>
        /// <param name="consents">List of <see cref="ConsentModel">consent</see> objects that need to be saved</param>
        /// <response code="200">A success response object.</response>
        [DualPost("save-consents", Name = RouteNames.SaveGdprPreferences, OnlyApi = true)]
        [RejectForImitator]
        [AllowAnonymous]
        public IActionResult SaveConsents(IList<ConsentModel> consents)
        {
            if (consents == null)
                return BadRequest();
            if (consents.Any())
            {
                //if user is not logged in, guest signin
                ApplicationEngine.GuestSignIn();
                var consentDictionary = consents.Where(x => x.Id != 0).ToDictionary(x => x.Id, x => x.ConsentStatus);
                _gdprService.SetUserConsents(CurrentUser.Id, consentDictionary);
            }
            else
            {
                if (CurrentUser == null)
                {
                    //there were no consents, so just create a cookie to track
                    CookieHelper.SetResponseCookie(ApplicationConfig.ConsentCookieName, Guid.NewGuid().ToString(), false);
                }

            }
         
            return R.Success.Result;
        }

    }
}