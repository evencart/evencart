using System.Net;
using System.Threading.Tasks;
using EvenCart.Core;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;
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

            var blockRequest = bannedIps != null && bannedIps.Contains(ip);

            if (!blockRequest)
            {
                //is admin area ip restricted?
                var adminIps = _securitySettings.GetAdminRestrictedIps();
                var isAdminArea = ApplicationEngine.IsAdmin();
                if (isAdminArea && adminIps != null && !adminIps.Contains(ip))
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