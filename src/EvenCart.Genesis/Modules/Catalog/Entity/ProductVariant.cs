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
using Genesis.Data;

namespace EvenCart.Data.Entity.Shop
{
    public class ProductVariant : GenesisEntity
    {
        public int ProductId { get; set; }

        public string Sku { get; set; }

        public string Gtin { get; set; }

        public string Mpn { get; set; }

        public decimal? Price { get; set; }

        public decimal? ComparePrice { get; set; }

        public bool TrackInventory { get; set; }

        public bool CanOrderWhenOutOfStock { get; set; }

        public int MediaId { get; set; }

        public bool DisableSale { get; set; }

        #region Virtual Properties
        public virtual IList<ProductVariantAttribute> ProductVariantAttributes { get; set; }

        public virtual IList<WarehouseInventory> Inventories { get; set; }
        #endregion
    }
}