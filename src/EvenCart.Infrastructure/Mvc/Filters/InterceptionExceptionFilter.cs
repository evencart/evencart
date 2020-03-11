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
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvenCart.Infrastructure.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class InterceptionExceptionFilter : ExceptionFilterAttribute
    {
        /*public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(InterceptorException))
            {
                var exception = actionExecutedContext.Exception as InterceptorException;
                if (exception != null)
                {
                    //parse json result
                    var jsonResult = (JsonResult<FoundationResponseModel>) exception.OriginalResult;
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, jsonResult.Content);
                    actionExecutedContext.Response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    
                }
                   
                
            }
            base.OnException(actionExecutedContext);
        }*/
    }
}