using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace RoastedMarketplace.Infrastructure.Extensions
{
    public static class ActionContextExtensions
    {
        public static bool IsAdminArea(this ActionContext actionContext)
        {
            var descriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
            var areaName = descriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            return areaName == "admin";
        }
    }
}