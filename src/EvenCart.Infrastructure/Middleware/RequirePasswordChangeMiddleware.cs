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

using System.Net;
using System.Threading.Tasks;
using EvenCart.Core;
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Database;
using EvenCart.Data.Entity.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace EvenCart.Infrastructure.Middleware
{
    public class RequirePasswordChangeMiddleware
    {
        private readonly RequestDelegate _next;
        public RequirePasswordChangeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (DatabaseManager.IsDatabaseInstalled())
            {
                var currentUser = ApplicationEngine.CurrentUser;
                if (currentUser != null && currentUser.RequirePasswordChange &&
                    !IsChangePasswordUrl((context.Request.Path.Value)))
                {
                    context.Response.Redirect("/password-reset");
                    return;
                }
            }
            await _next.Invoke(context);
        }

        private bool IsChangePasswordUrl(string url)
        {
            return url == "/password-reset";
        }

    }
}