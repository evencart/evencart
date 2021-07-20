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
using System.Linq;
using EvenCart.Services.Products;
using Genesis;
using Genesis.Extensions;
using Genesis.Modules.Web;
using Genesis.Routing;

namespace EvenCart.Seo
{
    public class SitemapProvider : ISitemapProvider
    {

        public SitemapProvider()
        {
        }

        public IList<string> GetUrls()
        {
            var urls = new List<string>()
            {
                GenesisEngine.Instance.RouteUrl(RouteNames.Home, null, true)
            };

            //product urls
            urls = urls.Concat(AddProductUrls())
                .Concat(AddContentPageUrls())
                .ToList();

            return urls;
        }

        private IEnumerable<string> AddProductUrls()
        {
            var productService = D.Resolve<IProductService>();
            var productUrls = productService.Get(x => x.Published && !x.Deleted).Select(x =>
                GenesisEngine.Instance.RouteUrl(RouteNames.SingleProduct, new {seName = x.SeoMeta.Slug, id = x.Id}, true));

            return productUrls;
        }

        private IEnumerable<string> AddContentPageUrls()
        {
            var contentPageService = D.Resolve<IContentPageService>();
            var urls = contentPageService.Get(x => x.Published).ToList().GetWithParentTree().Select(x =>
                GenesisEngine.Instance.RouteUrl(RouteNames.SinglePage,
                    new {seName = x.SeoMeta.Slug, id = x.Id}, true));

            return urls;
        }
    }
}