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

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvenCart.Infrastructure.ViewEngines
{
    public interface IViewAccountant
    {
        string GetThemeViewPath(string viewName, bool ignoreAdminViews = false);

        IList<string> GetAllMatchingViewPaths(string viewName);

        IList<string> GetSearchLocations();

        string RenderView(ViewContext viewContext);

        string RenderView(string viewName, string originalViewPath, string area, object parameters = null);

        string RenderView(string viewName, string htmlContent, object parameters = null);

        CachedView GetView(string viewName, string requestedPath, string area, object parameters = null);

        string GetLayoutPath(string layoutName);

        Dictionary<string, object> CompileAllViews(string controller = null, string area = null, bool splitted = false);

        void ClearCachedViews();
    }
}