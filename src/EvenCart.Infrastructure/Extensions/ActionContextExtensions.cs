using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace EvenCart.Infrastructure.Extensions
{
    public static class ActionContextExtensions
    {
        public static bool IsAdminArea(this ActionContext actionContext)
        {
            if (actionContext == null)
            {
                return ApplicationEngine.CurrentHttpContext.Request.Path.Value.StartsWith(
                    "/" + ApplicationConfig.AdminAreaName);
            }
            var descriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
            var areaName = descriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            return areaName == "admin";
        }
    }
}