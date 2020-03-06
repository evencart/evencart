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
using EvenCart.Infrastructure.Mvc.Models;

namespace EvenCart.Areas.Administration.Models.Orders
{
    /// <summary>
    /// The order fulfillment group object
    /// </summary>
    public class OrderFulfillmentListModel : FoundationModel
    {
        /// <summary>
        /// The <see cref="WarehouseModel">warehouse</see> in this fulfillment
        /// </summary>
        public WarehouseModel Warehouse { get; set; }

        /// <summary>
        /// A list of <see cref="OrderItemModel">order item</see> objects in this fulfillment
        /// </summary>
        public IList<OrderItemModel> OrderItems { get; set; }

    }
}