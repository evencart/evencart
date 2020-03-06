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

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EvenCart.Core;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class IpAddressValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecuritySettings _securitySettings;
        public IpAddressValidationMiddleware(RequestDelegate next, SecuritySettings securitySettings)
        {
            _next = next;
            _securitySettings = securitySettings;
        }

        public async Task Invoke(HttpContext context)
        {
            //get the current ip 
            var bannedIps = _securitySettings.GetBannedIps();
            //is it one of the banned ips?
            var ip = WebHelper.GetClientIpAddress();

            var blockRequest = bannedIps != null && bannedIps.Any() && bannedIps.Contains(ip);

            if (!blockRequest)
            {
                //is admin area ip restricted?
                var adminIps = _securitySettings.GetAdminRestrictedIps();
                var isAdminArea = ApplicationEngine.IsAdmin();
                if (isAdminArea && adminIps != null && adminIps.Any() && !adminIps.Contains(ip))
                {
                    blockRequest = true;
                }
            }

            //block if required
            if (blockRequest)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _next(context);
        }
    }
}