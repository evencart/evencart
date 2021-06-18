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

using Genesis.Infrastructure.Mvc.Models;
using Genesis.Modules.Meta;

namespace EvenCart.Areas.Administration.Models.Shop
{
    public class ProductSearchModel : AdminSearchModel
    {
        public int[] CategoryIds { get; set; }

        public int[] ManufacturerIds { get; set; }

        public int[] VendorIds { get; set; }

        public string SortColumn { get; set; }

        public SortOrder SortOrder { get; set; }

        public bool? Published { get; set; }

        public string[] Tags { get; set; }

        public int[] CatalogIds { get; set; }
    }
}