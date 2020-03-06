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