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

using System.Linq;
using EvenCart.Data.Entity.Shop;

namespace EvenCart.Services.Extensions
{
    public static class CategoryExtensions
    {
        public static bool IsAnyChild(this Category category, int childCategoryId)
        {
            if (category?.Children == null)
                return false;

            return category.Children.Any(x => x.Id == childCategoryId || IsAnyChild(x, childCategoryId));
        }

        public static string GetCategoryPath(this Category category)
        {
            if (category == null)
                return null;
            var c = category.Parent;
            var categoryPath = "";
            while (c != null)
            {
                categoryPath = $"{c.SeoMeta.Slug}/" + categoryPath;
                c = c.Parent;
            }
            categoryPath = categoryPath.TrimEnd('/');
            return categoryPath;
        }
    }
}