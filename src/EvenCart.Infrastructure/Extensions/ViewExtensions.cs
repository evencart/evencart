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

using DotLiquid;
using EvenCart.Core.Infrastructure;
using EvenCart.Infrastructure.MediaServices;
using EvenCart.Infrastructure.ViewEngines;

namespace EvenCart.Infrastructure.Extensions
{
    public static class ViewExtensions
    {
        public static bool IsLayoutTemplate(this Template template)
        {
            return false;
        }

        public static bool HasLayout(this Template template)
        {
            return false;
        }

        public static ViewSplited ToSplited(this CachedView cachedView)
        {
            if (cachedView == null)
                return null;
            var htmlProcessor = DependencyResolver.Resolve<IHtmlProcessor>();
            var body = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/body");
            var title = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head/title", true);
            var head = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head");
            var description = htmlProcessor.GetContentByXPath(cachedView.Raw, @"/html/head/meta[@name=""description""]");
            return new ViewSplited() {
                BodyHtml = body,
                Description = description,
                HeadHtml = head,
                Title = title
            };
        }
    }
}