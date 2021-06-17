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
using EvenCart.Areas.Administration.Models.Warehouse;
using Genesis.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// Represents order fulfillment details of an order
    /// </summary>
    public class OrderFulfillmentEditorModel : GenesisModel
    {
        /// <summary>
        /// The <see cref="WarehouseModel">warehouse</see> of the fulfillment
        /// </summary>
        public WarehouseModel Warehouse { get; set; }

        /// <summary>
        /// A collection of <see cref="OrderItemFulfillmentModel">order item</see> object
        /// </summary>
        public IList<OrderItemFulfillmentModel> OrderItems { get; set; }
        /// <summary>
        /// The single order item fulfillment object
        /// </summary>
        public class OrderItemFulfillmentModel : GenesisModel
        {
            /// <summary>
            /// A single <see cref="OrderItemModel">order item</see> object
            /// </summary>
            public OrderItemModel OrderItem { get; set; }
            /// <summary>
            /// The currency quantity of the item in the warehouse
            /// </summary>
            public int CurrentQuantity { get; set; }

            /// <summary>
            /// The available quantity of the item in the warehouse
            /// </summary>
            public int AvailableQuantity { get; set; }

            /// <summary>
            /// Specifies if the order fulfillment is locked for editing
            /// </summary>
            public bool Locked { get; set; }
        }
    }
}