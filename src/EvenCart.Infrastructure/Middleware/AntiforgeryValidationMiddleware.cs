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
using EvenCart.Core.Infrastructure;
using EvenCart.Data.Entity.Settings;
using EvenCart.Data.Extensions;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc;
using EvenCart.Services.Security;
using EvenCart.Services.Serializers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace EvenCart.Infrastructure.Middleware
{
    public class AntiforgeryValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;
        private const string VerificationKeyHeader = "X-API-VERIFICATION";
        public AntiforgeryValidationMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
#if RELEASEFORDEMO
            //for our demo site, we don't allow any post requests except to the login page. all the other requests are blocked
            if (HttpMethods.IsPost(context.Request.Method) && context.Request.Path != "/api/authentication/login" )
            {
                var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(dataSerializer.Serialize(new
                {
                    success = false,
                    message = "The demo site is read-only. No data update is allowed."
                }));
                return;
            }
#endif
            //is the url excluded from validation
            if (!ApplicationConfig.AntiforgerySparedUrls.Contains(context.Request.Path))
                if (HttpMethods.IsPost(context.Request.Method) && !context.IsTokenAuthenticated())
                {
                    //we make sure that any request that comes to us either has a antiforgery token (which means that it's a web based request)
                    //or we have a header set for verification which will mean that the api is being called from some other client such as an APP
                    //If latter is the case, we'll only allow requests if app key is provided. This makes sure that somebody 
                    //can bruteforce the public endpoints such as registration, catalog etc. without token
                    //the client may have sent api verification key as well...check if that is the case
                    var validated = false;

                    var headerPresent =
                        context.Request.Headers.TryGetValue(VerificationKeyHeader, out var headerValues);
                    if (headerPresent)
                    {
                        //let's validate the header value
                        var securitySettings = DependencyResolver.Resolve<SecuritySettings>();
                        var cryptographyService = DependencyResolver.Resolve<ICryptographyService>();
                        //calculate hash 
                        var value = headerValues.FirstOrDefault();
                        if (!value.IsNullEmptyOrWhiteSpace())
                        {
                            var hashedHeader = cryptographyService.GetMd5Hash(value);
                            validated = hashedHeader == securitySettings.SharedVerificationKey;
                        }
                    }

                    //if not validated, validate antiforgery
                    if (!validated)
                    {
                        try
                        {
                            await _antiforgery.ValidateRequestAsync(context);
                        }
                        catch
                        {
                            var dataSerializer = DependencyResolver.Resolve<IDataSerializer>();
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(dataSerializer.Serialize(new
                            {
                                success = false,
                                error = "Unable to verify the request",
                                error_code = ErrorCodes.AntiForgeryValidationFailed
                            }));
                            return;
                        }
                    }
                }

            await _next(context);
        }
    }
}