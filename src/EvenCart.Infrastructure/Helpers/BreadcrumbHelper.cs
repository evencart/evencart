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
using EvenCart.Data.Entity.Shop;
using EvenCart.Infrastructure.Extensions;
using EvenCart.Infrastructure.Mvc.Breadcrumbs;
using EvenCart.Infrastructure.Routing;
using EvenCart.Services.Extensions;

namespace EvenCart.Infrastructure.Helpers
{
    public static class BreadcrumbHelper
    {
        public static void SetCategoryBreadcrumb(Category category, IList<Category> allCategories)
        {
            var breadcrumbNodes = new List<BreadcrumbNode>();
            while (category != null)
            {
                breadcrumbNodes.Insert(0, new BreadcrumbNode()
                {
                    DisplayText = category.Name,
                    Description = category.Description,
                    Url = category.SeoMeta != null ? ApplicationEngine.RouteUrl(RouteNames.ProductsPage, new
                    {
                        seName = category.SeoMeta.Slug,
                        categoryPath = category.GetCategoryPath()
                    }, true) : ""
                });
                category = category.Parent;
            }

            foreach (var node in breadcrumbNodes)
                ApplicationEngine.CurrentHttpContext.AppendToBreadcrumb(node);
        }
    }
}