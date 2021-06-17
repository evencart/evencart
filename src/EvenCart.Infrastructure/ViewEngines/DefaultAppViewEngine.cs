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

using Genesis.Infrastructure;
using EvenCart.Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace EvenCart.Infrastructure.ViewEngines
{
    public class DefaultAppViewEngine : IAppViewEngine
    {
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (!viewName.StartsWith("Components"))
            {
                var controller = context.ActionDescriptor.RouteValues["controller"];
                viewName = $"{controller}/{viewName}";
            }

            return GetView(viewName, isMainPage);
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return GetView(viewPath, isMainPage);
        }

        private ViewEngineResult GetView(string viewName, bool isMainPage)
        {
            var viewAccountant = DependencyResolver.Resolve<IViewAccountant>();
            var viewFilePath = viewAccountant.GetThemeViewPath(viewName);
            if (!viewFilePath.IsNullEmptyOrWhiteSpace())
            {
                return ViewEngineResult.Found(viewFilePath, new RoastedLiquidView(viewFilePath, viewName, viewAccountant));
            }
            return ViewEngineResult.NotFound(viewName, viewAccountant.GetSearchLocations());
        }
    }
}