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