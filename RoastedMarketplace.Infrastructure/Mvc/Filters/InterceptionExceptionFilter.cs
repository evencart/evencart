#region Author Information
// InterceptionExceptionFilter.cs
// 
// Author 2016 Apexol Technologies. All Rights Reserved.
// 
// Created On: 04 07 2016 06:18 PM
// Last Modified: 04 07 2016 06:18 PM
#endregion

using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RoastedMarketplace.Infrastructure.Mvc.Filters
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