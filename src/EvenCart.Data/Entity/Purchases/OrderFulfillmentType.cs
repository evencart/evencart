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

namespace EvenCart.Data.Entity.Purchases
{
    /// <summary>
    /// The way in which order should be fulfilled
    /// </summary>
    public enum OrderFulfillmentType
    {
        /// <summary>
        /// Fulfill entire order from one warehouse
        /// </summary>
        WholeFromSingleWarehouse = 1,
        /// <summary>
        /// Fulfill order from multiple warehouses restricting a single product to one warehouse
        /// </summary>
        SplitToMultipleWarehouseByItem = 2,
        /// <summary>
        /// NSY: Fulfill order from multiple warehouses allowing single product quantities to be fulfilled across warehouses.
        /// </summary>
        SplitToMultipleWarehouseByItemQuantity = 3
    }
}