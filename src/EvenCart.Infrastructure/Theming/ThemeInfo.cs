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

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EvenCart.Infrastructure.Theming
{
    public class ThemeInfo
    {
        public string Name { get; set; }

        public string DirectoryName { get; set; }

        public string Description { get; set; }

        public string ProductBoxImageSize { get; set; }

        public string CartItemImageSize { get; set; }

        public string ProductPageImageSize { get; set; }

        public string ProductPageImageThumbnailSize { get; set; }

        public string UserProfileImageSize { get; set; }

        public Dictionary<string, string> WidgetZones { get; set; }

        public ConcurrentDictionary<string, string> Templates { get; set; } = new ConcurrentDictionary<string, string>();

        public string ThumbnailUrl { get; set; }

        public bool PendingRestart { get; set; }

        public string GetTemplatePath(string templateKey)
        {
            Templates.TryGetValue(templateKey, out var template);
            return template;
        }
    }
}