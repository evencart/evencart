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
using EvenCart.Data.Database;
using EvenCart.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvenCart.Infrastructure.Security.Attributes
{
    /// <summary>
    /// Executes an action only if the logged in user has the provided capabilities
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CapabilityRequiredAttribute : ActionFilterAttribute
    {
        private readonly string[] _capabilityName;

        public CapabilityRequiredAttribute(params string[] capabilityName)
        {
            _capabilityName = capabilityName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException();
            if (!DatabaseManager.IsDatabaseInstalled())
            {
                base.OnActionExecuting(context);
                return;
            }
          
            //get the current user
            var currentUser = ApplicationEngine.CurrentUser;

            if (currentUser == null || !currentUser.Can(_capabilityName))
                context.Result = new UnauthorizedResult();
        }

       
    }
}