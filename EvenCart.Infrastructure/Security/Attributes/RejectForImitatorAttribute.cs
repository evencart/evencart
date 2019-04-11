using System;
using EvenCart.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvenCart.Infrastructure.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RejectForImitatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (ApplicationEngine.CurrentUser.IsImitator(out _))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}