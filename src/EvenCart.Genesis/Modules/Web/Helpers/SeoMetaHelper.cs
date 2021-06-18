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

using EvenCart.Data.Entity.Shop;
using Genesis;
using Genesis.Infrastructure;
using Genesis.Modules.Web;
using Genesis.Routing;

namespace EvenCart.Genesis.Modules.Web
{
    public static class SeoMetaHelper
    {
        public static string GetUrl(SeoMeta seoMeta)
        {
            var engine = D.Resolve<IGenesisEngine>();
            switch(seoMeta.EntityName)
            {
                case nameof(Product):
                    return engine.RouteUrl(RouteNames.SingleProduct, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
                case nameof(Category):
                    return engine.RouteUrl(RouteNames.ProductsPage, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
                case nameof(ContentPage):
                    return engine.RouteUrl(RouteNames.SinglePage, new { seName = seoMeta.Slug, id = seoMeta.EntityId });
            }

            return null;
        }

    }
}