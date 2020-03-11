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

using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Plugins
{
    public class MarketPluginInfoModel : FoundationModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string SystemName { get; set; }

        public string Author { get; set; }

        public string AuthorUrl { get; set; }

        public string PluginUrl { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPluginUrlExternal { get; set; }

        public string Tags { get; set; }

    }
}