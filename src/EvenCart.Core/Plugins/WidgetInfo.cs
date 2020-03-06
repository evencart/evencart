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

namespace EvenCart.Core.Plugins
{
    public class WidgetInfo
    {
        public string PluginName { get; set; }

        public string PluginSystemName { get; set; }

        public string WidgetSystemName { get; set; }

        public string WidgetDisplayName { get; set; }

        public IList<string> WidgetZones { get; set; }

        public string DisplayOrder { get; set; }

        public string Id { get; set; }

        public bool HasConfiguration { get; set; }

        public bool SkipDragging { get; set; }

        public string ConfigurationUrl { get; set; }

        public IWidget WidgetInstance { get; set; }
    }
}