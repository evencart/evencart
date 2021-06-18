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

namespace EvenCart.Models.Shipments
{
    public class ShipmentItemModel : GenesisModel
    {
        public int OrderItemId { get; set; }

        public string ProductName { get; set; }

        public string SeName { get; set; }

        public string AttributeText { get; set; }

        public int OrderedQuantity { get; set; }

        public int ShippedQuantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}