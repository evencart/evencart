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

namespace EvenCart.Areas.Administration.Models.Warehouse
{
    /// <summary>
    /// The inventory object
    /// </summary>
    public class InventoryModel : FoundationModel
    {
        /// <summary>
        /// The identifier for a single item. Can be a product id or variant id depending on if the product has variants
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id of the warehouse
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// The total quantity of the item
        /// </summary>
        public int TotalQuantity { get; set; }
    }
}