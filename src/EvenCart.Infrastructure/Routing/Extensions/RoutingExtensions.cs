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

using EvenCart.Data.Entity.Pages;

namespace EvenCart.Infrastructure.Routing.Extensions
{
    public static class RoutingExtensions
    {
        public static string GetParentPath(this ContentPage contentPage)
        {
            if (contentPage == null)
                return null;
            var c = contentPage.Parent;
            var path = "";
            while (c != null)
            {
                path = $"{c.SeoMeta.Slug}/" + path;
                c = c.Parent;
            }
            path = path.TrimEnd('/');
            return path;
        }
    }
}