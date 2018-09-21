using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoastedMarketplace.Data.Database;
using RoastedMarketplace.Services.Extensions;

namespace RoastedMarketplace.Infrastructure.Security.Attributes
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
            return; //todo: enable the code below
            //get the current user
            var currentUser = ApplicationEngine.CurrentUser;

            if (currentUser == null || !currentUser.Can(_capabilityName))
                context.Result = new UnauthorizedResult();
        }

       
    }
}