#region Author Information
// AuthorizedAdministratorAttribute.cs
// 
// (c) 2016 Apexol Technologies. All Rights Reserved.
// 
#endregion

using System;
using EvenCart.Data.Constants;
using EvenCart.Data.Database;
using EvenCart.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvenCart.Infrastructure.Security.Attributes
{
    /// <summary>
    /// Specifies tha logged in user must be administrator to access this area
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAdministratorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!DatabaseManager.IsDatabaseInstalled())
            {
                base.OnActionExecuting(context);
                return;
            }
            if (!ApplicationEngine.CurrentUser.Can(CapabilitySystemNames.AccessAdministration))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}